using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace SGSControls
{
    class DogEar
    {
        Microsoft.DirectX.DirectSound.Device dev = new Microsoft.DirectX.DirectSound.Device();

        private List<AudioClip> audioList = new List<AudioClip>();
        private string mediaFile;

        public double CatCoef { get; set; }
        public double RabbitCoef { get; set; }

        public double Hanning_Duration { get; set; }
        public double Hanning_Overlap { get; set; }
        public double Delta_Divisor { get; set; }

        public DogEar(System.Windows.Forms.Control owner,string mediafile, string FFMpegPath)
        {
            mediaFile = mediafile;
            AudioFileIO.FFmpegpath = FFMpegPath;
            dev.SetCooperativeLevel(owner, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);
        }

        public void EarAClip(double begin, double duration, EarType type)
        {
            AudioClip clip = audioList.FirstOrDefault(t => t.Equivalent(begin, duration, type));
            if (clip == null)
            {
                clip = new AudioClip
                           {
                               BeginTime = begin,
                               Duration = duration,
                               Type = type == EarType.Human ? EarType.Cat : type
                           };
                var wavInfo = new wavinfo();
                var rawStream = AudioFileIO.ExtractWave(mediaFile, ref wavInfo, begin.ToString("F1"), duration.ToString("F1"));
                clip.OriginalStream = new MemoryStream();
                clip.ScaledStream = new MemoryStream();
                AudioFileIO.WriteHead(clip.OriginalStream, wavInfo);
                var buff = new byte[2048];
                rawStream.Seek(0, SeekOrigin.Begin);
                int read;
                do
                {
                    read = rawStream.Read(buff, 0, 2048);
                    clip.OriginalStream.Write(buff, 0, read);
                } while (read > 0);
                AudioFileIO.WriteStreamLen(clip.OriginalStream);
                
                switch (clip.Type)
                {
                    case EarType.Cat:
                        WSOLA.ScaleAudio(rawStream, clip.ScaledStream, CatCoef, Hanning_Duration, Hanning_Overlap, Delta_Divisor, wavInfo);
                        break;
                    case EarType.Rabbit:
                        WSOLA.ScaleAudio(rawStream, clip.ScaledStream, RabbitCoef, Hanning_Duration, Hanning_Overlap, Delta_Divisor, wavInfo);
                        break;
                }
                audioList.Add(clip);
            }
            //play it
            if (type != EarType.Human)
            {
                clip.ScaledStream.Seek(0, SeekOrigin.Begin);
                var secbuf = new Microsoft.DirectX.DirectSound.SecondaryBuffer(clip.ScaledStream, dev);
                secbuf.Play(0, Microsoft.DirectX.DirectSound.BufferPlayFlags.Default);
            }
            else
            {
                clip.OriginalStream.Seek(0, SeekOrigin.Begin);
                var secbuf = new Microsoft.DirectX.DirectSound.SecondaryBuffer(clip.OriginalStream, dev);
                secbuf.Play(0, Microsoft.DirectX.DirectSound.BufferPlayFlags.Default);
            }
        }



    }

    enum EarType { Human = 0, Cat, Rabbit }

    class AudioClip
    {
        public EarType Type = EarType.Human;
        public Stream OriginalStream = null;
        public Stream ScaledStream = null;
        public double BeginTime = 0;
        public double Duration = 0;
        public bool Equivalent(AudioClip clip)
        {
            if (Math.Abs(BeginTime - clip.BeginTime) < 0.1 &&
                Math.Abs(Duration - clip.Duration) < 0.1 &&
                Type == clip.Type) 
                return true;
            return false;
        }

        public bool Equivalent(double beginTime, double duration, EarType type)
        {
            if (Math.Abs(BeginTime - beginTime) < 0.1 &&
                Math.Abs(Duration - duration) < 0.1 &&
                (Type == type || type == EarType.Human))
                return true;
            return false;
        }
    }


    static class AudioFileIO
    {
        public static string FFmpegpath;
        public static Stream ExtractWave(string videofilename, ref wavinfo wavinf, string begin, string len)
        {
            //ffmpeg -i <infile> -f wav -ac 1 -vn -y <outfile.wav>
            if (FFmpegpath == null) throw new Exception("FFmpegpath is not set");
            var audioStream = new MemoryStream();
            var ffmpegprocess = new Process
                                    {
                                        StartInfo =
                                            {
                                                FileName = FFmpegpath,
                                                Arguments =
                                                    "-i \"" + videofilename + "\" -ss " + begin + " -t " + len +
                                                    " -f wav -ac 1 -ar 48000 -vn -y -",
                                                CreateNoWindow = true,
                                                RedirectStandardOutput = true,
                                                UseShellExecute = false
                                            }
                                    };
            ffmpegprocess.Start();
            var stdout = ffmpegprocess.StandardOutput.BaseStream;

            var buf = new byte[4];

            int read;
            Tagname tag;
            UInt32 taglen;
            read = stdout.Read(buf, 0, 4);
            tag = gettagname(buf); //RIFF
            if (tag != Tagname.RIFF) throw new Exception("WaveReadError");

            read = stdout.Read(buf, 0, 4);
            taglen = gettaglen(buf); //0

            read = stdout.Read(buf, 0, 4);
            tag = gettagname(buf); //WAVE
            if (tag != Tagname.WAVE) throw new Exception("WaveReadError");

            read = stdout.Read(buf, 0, 4);
            tag = gettagname(buf); //fmt
            if (tag != Tagname.FMT) throw new Exception("WaveReadError");
            read = stdout.Read(buf, 0, 4);
            taglen = gettaglen(buf);  //length;
            var chunk = new byte[taglen];
            read = stdout.Read(chunk, 0, (int)taglen);
            wavinfo info = parsefmt(chunk);
            info.CloneTo(ref wavinf);

            read = stdout.Read(buf, 0, 4);
            tag = gettagname(buf); //data
            read = stdout.Read(buf, 0, 4);
            taglen = gettaglen(buf);  //length;
            if (tag != Tagname.DATA)
            {
                throw new Exception("Please report bug: Strange wave format");
            }
            if (wavinf.BitPerSample != 16)
            {
                throw new Exception("Please report bug: 8 bit wav");
            }

            int samplelen = (int)(wavinf.Frequency / 5); //0.1 秒音频的数据(字节数，所以是除以5)
            chunk = new byte[samplelen];

            do
            {
                read = stdout.Read(chunk, 0, samplelen);
                audioStream.Write(chunk, 0, read);
            } while (read > 0);
            return audioStream;

        }
        public static void WriteHead(Stream oStream, wavinfo info)
        {
            var seg = new byte[4];
            byte[] buff;

            System.Text.Encoding.ASCII.GetBytes("RIFF", 0, 4, seg, 0);
            oStream.Write(seg, 0, 4);


            buff = System.BitConverter.GetBytes((Int32)0);
            oStream.Write(buff, 0, buff.Length);
            System.Text.Encoding.ASCII.GetBytes("WAVE", 0, 4, seg, 0);
            oStream.Write(seg, 0, 4);

            System.Text.Encoding.ASCII.GetBytes("fmt ", 0, 4, seg, 0);
            oStream.Write(seg, 0, 4);
            buff = System.BitConverter.GetBytes((UInt32)16);
            oStream.Write(buff, 0, buff.Length);
            buff = System.BitConverter.GetBytes((UInt16)0x01);
            oStream.Write(buff, 0, buff.Length);
            buff = System.BitConverter.GetBytes((UInt16)info.Channels);
            oStream.Write(buff, 0, buff.Length);
            buff = System.BitConverter.GetBytes((UInt32)info.Frequency);
            oStream.Write(buff, 0, buff.Length);
            buff = System.BitConverter.GetBytes((UInt32)info.ByteRate);
            oStream.Write(buff, 0, buff.Length);
            buff = System.BitConverter.GetBytes((UInt16)info.BlockAlign);
            oStream.Write(buff, 0, buff.Length);
            buff = System.BitConverter.GetBytes((UInt16)info.BitPerSample);
            oStream.Write(buff, 0, buff.Length);


            System.Text.Encoding.ASCII.GetBytes("data", 0, 4, seg, 0);
            oStream.Write(seg, 0, 4);
            buff = System.BitConverter.GetBytes((UInt32)0);
            oStream.Write(buff, 0, buff.Length);


        }
        public static void WriteStreamLen(Stream oStream)
        {
            byte[] buff;
            int fmtlen;
            oStream.Seek(0x10, SeekOrigin.Begin);
            fmtlen = oStream.ReadByte();

            oStream.Seek(4, SeekOrigin.Begin);
            buff = System.BitConverter.GetBytes((UInt32)(oStream.Length - 0x08));
            oStream.Write(buff, 0, buff.Length);
            if (fmtlen == 16)
            {
                oStream.Seek(0x28, SeekOrigin.Begin);
                buff = System.BitConverter.GetBytes((UInt32)(oStream.Length - 0x28));
                oStream.Write(buff, 0, buff.Length);
            }
            else
            {
                oStream.Seek(0x2A, SeekOrigin.Begin);
                buff = System.BitConverter.GetBytes((UInt32)(oStream.Length - 0x2A));
                oStream.Write(buff, 0, buff.Length);
            }
            oStream.Seek(0, SeekOrigin.Begin);

        }


        enum Tagname { RIFF, WAVE, FMT, DATA, Unknown };
        private static Tagname gettagname(byte[] seg)
        {
            //[RIFF]
            string segname = System.Text.Encoding.ASCII.GetString(seg, 0, 4);
            var tgn = (Tagname)Enum.Parse(typeof(Tagname), segname.TrimEnd().ToUpper());
            return tgn;

        }
        private static UInt32 gettaglen(byte[] seg)
        {
            UInt32 len;
            len = (UInt32)seg[0] + ((UInt32)(seg[1]) << 8) + ((UInt32)(seg[2]) << 16) + ((UInt32)(seg[3]) << 24);
            return len;
        }
        private static wavinfo parsefmt(byte[] chunk)
        {
            wavinfo wi = new wavinfo();
            wi.FormatTag = (UInt32)chunk[0] + ((UInt32)(chunk[1]) << 8);
            wi.Channels = (UInt32)chunk[2] + ((UInt32)(chunk[3]) << 8);
            wi.Frequency = (UInt32)chunk[4] + ((UInt32)(chunk[5]) << 8) + ((UInt32)(chunk[6]) << 16) + ((UInt32)(chunk[7]) << 24);
            wi.ByteRate = (UInt32)chunk[8] + ((UInt32)(chunk[9]) << 8) + ((UInt32)(chunk[10]) << 16) + ((UInt32)(chunk[11]) << 24);
            wi.BlockAlign = (UInt32)chunk[12] + ((UInt32)(chunk[13]) << 8);
            wi.BitPerSample = (UInt32)chunk[14] + ((UInt32)(chunk[15]) << 8);
            return wi;
        }

    }

    class wavinfo
    {
        public UInt32 FormatTag = 0;
        public UInt32 Channels = 0;
        public UInt32 Frequency = 0;
        public UInt32 BitPerSample = 0;
        public UInt32 ByteRate = 0;
        public UInt32 BlockAlign = 0;
        public void CloneTo(ref wavinfo cloneTo)
        {
            cloneTo.BitPerSample = BitPerSample;
            cloneTo.BlockAlign = BlockAlign;
            cloneTo.ByteRate = ByteRate;
            cloneTo.Channels = Channels;
            cloneTo.FormatTag = FormatTag;
            cloneTo.Frequency = Frequency;
        }

    }


    class WSOLA
    {
        static public void ScaleAudio(Stream src, Stream dst, double coef, double hdur, double hover, double del, wavinfo wavinf)
        {
            int isize;
            int osize;
            int readoff = 0, readn = 0;
            int nread;

            int writesize;
            byte[] inputbuf;
            byte[] outputbuf;

            WSOLA.setWSOLAPara(hdur, hover, del, wavinf.Frequency);

            WSOLA.initWSOLA((int)wavinf.Channels, (int)wavinf.Frequency, coef);
            isize = WSOLA.getInputSize();
            osize = WSOLA.getOutputSize();
            inputbuf = new byte[isize];
            outputbuf = new byte[osize];
            osize = WSOLA.getOutputSize();
            WSOLA.prereadSrc(ref readoff, ref readn);
            src.Seek(readoff, SeekOrigin.Begin);
            nread = src.Read(inputbuf, 0, readn);
            WSOLA.initProcess(inputbuf);
            //beginloop

            writesize = osize / 2;
            dst.Seek(0,SeekOrigin.Begin);
            AudioFileIO.WriteHead(dst, wavinf);


            do
            {
                WSOLA.prereadSrc(ref readoff, ref readn);
                src.Seek(readoff, SeekOrigin.Begin);
                nread = src.Read(inputbuf, 0, readn);
                if (nread < readn) break;
                WSOLA.loadSource(inputbuf);

                WSOLA.prereadDes(ref readoff, ref readn);
                src.Seek(readoff, SeekOrigin.Begin);
                nread = src.Read(inputbuf, 0, readn);
                if (nread < readn) break;

                WSOLA.loadDesire(inputbuf);
                WSOLA.process(outputbuf);
                dst.Write(outputbuf, 0, writesize);
            } while (true);
            AudioFileIO.WriteStreamLen(dst);
            dst.Flush();
            WSOLA.destroyWsola();

        }


        [DllImport("wsolalib.dll")]
        private static extern void setWSOLAPara(double Hanning_Duration, double Hanning_Overlap, double Delta_Divisor, double Sample_Frequency);

        [DllImport("wsolalib.dll")]
        private static extern void initWSOLA(int channels, int frequency, double tau);

        [DllImport("wsolalib.dll")]
        private static extern int getOutputSize();

        [DllImport("wsolalib.dll")]
        private static extern int getInputSize();

        [DllImport("wsolalib.dll")]
        private static extern void initProcess([MarshalAs(UnmanagedType.LPArray)]Byte[] input);

        [DllImport("wsolalib.dll")]
        private static extern void prereadSrc(ref int srcIndex, ref int srcLen);

        [DllImport("wsolalib.dll")]
        private static extern void prereadDes(ref int desIndex, ref int desLen);

        [DllImport("wsolalib.dll")]
        private static extern void loadSource([MarshalAs(UnmanagedType.LPArray)]Byte[] input);

        [DllImport("wsolalib.dll")]
        private static extern void loadDesire([MarshalAs(UnmanagedType.LPArray)]Byte[] input);

        [DllImport("wsolalib.dll")]
        private static extern void process([MarshalAs(UnmanagedType.LPArray)]Byte[] output);

        [DllImport("wsolalib.dll")]
        private static extern void destroyWsola();
    }

}
