using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace SGS.Controls
{
    class WSOLA
    {
        Microsoft.DirectX.DirectSound.Device dev = new Microsoft.DirectX.DirectSound.Device();

        private readonly List<AudioClip> _audioList = new List<AudioClip>();
        private readonly string _mediaFile;

        public double SlowCoef { get; set; }
      //  public double RabbitCoef { get; set; }

        public double Hanning_Duration { get; set; }
        public double Hanning_Overlap { get; set; }
        public double Delta_Divisor { get; set; }

        public int MaxBuffer = 10;

        public WSOLA(System.Windows.Forms.Control owner,string mediafile)
        {
            _mediaFile = mediafile;
            AudioFileIO.FFmpegpath = Datatype.SGSConfig.FFMpegPath;
            dev.SetCooperativeLevel(owner, Microsoft.DirectX.DirectSound.CooperativeLevel.Normal);
        }

        public void EarAClip(double begin, double duration)
        {
            AudioClip clip = _audioList.FirstOrDefault(t => t.Equivalent(begin, duration));
            if (clip == null)
            {
                clip = new AudioClip
                           {
                               BeginTime = begin,
                               Duration = duration,
                           };
                var wavInfo = new Wavinfo();
                var rawStream = AudioFileIO.ExtractWave(_mediaFile, ref wavInfo, begin.ToString("F1"),
                                                        duration.ToString("F1"));
                var origStream = new MemoryStream();
                clip.ScaledStream = new MemoryStream();
                AudioFileIO.WriteHead(origStream, wavInfo);
                var buff = new byte[2048];
                rawStream.Seek(0, SeekOrigin.Begin);
                int read;
                do
                {
                    read = rawStream.Read(buff, 0, 2048);
                    origStream.Write(buff, 0, read);
                } while (read > 0);
                AudioFileIO.WriteStreamLen(origStream);

                WSOLASupport.ScaleAudio(rawStream, clip.ScaledStream, SlowCoef, Hanning_Duration, Hanning_Overlap,
                                        Delta_Divisor, wavInfo);

                _audioList.Add(clip);
                if (_audioList.Count > MaxBuffer) _audioList.RemoveAt(0);
            }
            //play it

            clip.ScaledStream.Seek(0, SeekOrigin.Begin);
            var secbuf = new Microsoft.DirectX.DirectSound.SecondaryBuffer(clip.ScaledStream, dev);
            secbuf.Play(0, Microsoft.DirectX.DirectSound.BufferPlayFlags.Default);

        }



    }

   // enum EarType { Human = 0, Cat, Rabbit }

    class AudioClip
    {
//        public Stream OriginalStream = null;
        public Stream ScaledStream = null;
        public double BeginTime = 0;
        public double Duration = 0;
        public bool Equivalent(AudioClip clip)
        {
            if (Math.Abs(BeginTime - clip.BeginTime) < 0.1 &&
                Math.Abs(Duration - clip.Duration) < 0.1 ) 
                return true;
            return false;
        }

        public bool Equivalent(double beginTime, double duration)
        {
            if (Math.Abs(BeginTime - beginTime) < 0.1 &&
                Math.Abs(Duration - duration) < 0.1)
                return true;
            return false;
        }
    }


    static class AudioFileIO
    {
        public static string FFmpegpath;
        public static Stream ExtractWave(string videofilename, ref Wavinfo wavinf, string begin, string len)
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
            tag = Gettagname(buf); //RIFF
            if (tag != Tagname.RIFF) throw new Exception("WaveReadError");

            read = stdout.Read(buf, 0, 4);
            taglen = Gettaglen(buf); //0

            read = stdout.Read(buf, 0, 4);
            tag = Gettagname(buf); //WAVE
            if (tag != Tagname.WAVE) throw new Exception("WaveReadError");

            read = stdout.Read(buf, 0, 4);
            tag = Gettagname(buf); //fmt
            if (tag != Tagname.FMT) throw new Exception("WaveReadError");
            read = stdout.Read(buf, 0, 4);
            taglen = Gettaglen(buf);  //length;
            var chunk = new byte[taglen];
            read = stdout.Read(chunk, 0, (int)taglen);
            Wavinfo info = Parsefmt(chunk);
            info.CloneTo(ref wavinf);

            read = stdout.Read(buf, 0, 4);
            tag = Gettagname(buf); //data
            read = stdout.Read(buf, 0, 4);
            taglen = Gettaglen(buf);  //length;
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
        public static void WriteHead(Stream oStream, Wavinfo info)
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
        public static void ExportAudioClip(string videofilename, string begin, string len, string bitrate, string outfilename)
        {
            //ffmpeg.exe -strict experimental  -f mp4 -acodec aac -ac 2 -ab 160k -vn -y $audiofile -i $file
            if (FFmpegpath == null) throw new Exception("FFmpegpath is not set");
            var argument = string.Format("-i \"{0}\" -ss {1} -t {2} -f mp3 -acodec libmp3lame -ac 2 -ab {3} -vn -y \"{4}\"",
                                         videofilename, begin, len, bitrate, outfilename);
            var ffmpegprocess = new Process
                                    {
                                        StartInfo =
                                            {
                                                FileName = FFmpegpath,
                                                Arguments =argument,
                                                CreateNoWindow = true,
                                                RedirectStandardOutput = true,
                                                UseShellExecute = false
                                            }
                                    };
            ffmpegprocess.Start();
            ffmpegprocess.WaitForExit();
        }

        enum Tagname { RIFF, WAVE, FMT, DATA, Unknown };
        private static Tagname Gettagname(byte[] seg)
        {
            string segname = System.Text.Encoding.ASCII.GetString(seg, 0, 4);
            var tgn = (Tagname)Enum.Parse(typeof(Tagname), segname.TrimEnd().ToUpper());
            return tgn;

        }
        private static UInt32 Gettaglen(byte[] seg)
        {
            uint len = BitConverter.ToUInt32(seg, 0);
            return len;
        }
        private static Wavinfo Parsefmt(byte[] chunk)
        {
            var wi = new Wavinfo
                         {
                             FormatTag = BitConverter.ToUInt16(chunk, 0),
                             Channels = BitConverter.ToUInt16(chunk, 2),
                             Frequency = BitConverter.ToUInt32(chunk, 4),
                             ByteRate = BitConverter.ToUInt32(chunk, 8),
                             BlockAlign = BitConverter.ToUInt16(chunk, 12),
                             BitPerSample = BitConverter.ToUInt16(chunk, 14)
                         };
            return wi;
        }

    }

    class Wavinfo
    {
        public UInt32 FormatTag = 0;
        public UInt32 Channels = 0;
        public UInt32 Frequency = 0;
        public UInt32 BitPerSample = 0;
        public UInt32 ByteRate = 0;
        public UInt32 BlockAlign = 0;
        public void CloneTo(ref Wavinfo cloneTo)
        {
            cloneTo.BitPerSample = BitPerSample;
            cloneTo.BlockAlign = BlockAlign;
            cloneTo.ByteRate = ByteRate;
            cloneTo.Channels = Channels;
            cloneTo.FormatTag = FormatTag;
            cloneTo.Frequency = Frequency;
        }

    }


    static class WSOLASupport
    {
        static public void ScaleAudio(Stream src, Stream dst, double coef, double hdur, double hover, double del, Wavinfo wavinf)
        {
            int isize;
            int osize;
            int readoff = 0, readn = 0;
            int nread;

            int writesize;
            byte[] inputbuf;
            byte[] outputbuf;

            setWSOLAPara(hdur, hover, del, wavinf.Frequency);

            initWSOLA((int)wavinf.Channels, (int)wavinf.Frequency, coef);
            isize = getInputSize();
            osize = getOutputSize();
            inputbuf = new byte[isize];
            outputbuf = new byte[osize];
            osize = getOutputSize();
            prereadSrc(ref readoff, ref readn);
            src.Seek(readoff, SeekOrigin.Begin);
            src.Read(inputbuf, 0, readn);
            initProcess(inputbuf);
            //beginloop

            writesize = osize / 2;
            dst.Seek(0,SeekOrigin.Begin);
            AudioFileIO.WriteHead(dst, wavinf);


            do
            {
                prereadSrc(ref readoff, ref readn);
                src.Seek(readoff, SeekOrigin.Begin);
                nread = src.Read(inputbuf, 0, readn);
                if (nread < readn) break;
                loadSource(inputbuf);

                prereadDes(ref readoff, ref readn);
                src.Seek(readoff, SeekOrigin.Begin);
                nread = src.Read(inputbuf, 0, readn);
                if (nread < readn) break;

                loadDesire(inputbuf);
                process(outputbuf);
                dst.Write(outputbuf, 0, writesize);
            } while (true);
            AudioFileIO.WriteStreamLen(dst);
            dst.Flush();
            destroyWsola();

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
