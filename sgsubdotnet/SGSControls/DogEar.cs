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
    }


    class AudioFileIO
    {
        public static string FFmpegpath = null;
        public static MemoryStream ExtractWave(string videofilename, ref wavinfo wavinf, string begin, string len)
        {
            //ffmpeg -i <infile> -f wav -ac 1 -vn -y <outfile.wav>
            if (FFmpegpath == null) throw new Exception("FFmpegpath is not set");
            MemoryStream audioStream = new MemoryStream();
            List<Byte[][]> wflist = new List<Byte[][]>();
            Process ffmpegprocess = new Process();
            ffmpegprocess.StartInfo.FileName = FFmpegpath;
            ffmpegprocess.StartInfo.Arguments = "-i \"" + videofilename + "\" -ss " + begin + " -t " + len + " -f wav -ac 1 -ar 48000 -vn -y -";
            ffmpegprocess.StartInfo.CreateNoWindow = true;
            ffmpegprocess.StartInfo.RedirectStandardOutput = true;
            ffmpegprocess.StartInfo.UseShellExecute = false;
            ffmpegprocess.Start();
            Stream stdout = ffmpegprocess.StandardOutput.BaseStream;
            byte[] buf;
            byte[] chunk;

            buf = new byte[4];

            int read;
            tagname tag;
            UInt32 taglen;
            read = stdout.Read(buf, 0, 4);
            tag = gettagname(buf); //RIFF
            if (tag != tagname.RIFF) throw new Exception("WaveReadError");

            read = stdout.Read(buf, 0, 4);
            taglen = gettaglen(buf); //0

            read = stdout.Read(buf, 0, 4);
            tag = gettagname(buf); //WAVE
            if (tag != tagname.WAVE) throw new Exception("WaveReadError");

            read = stdout.Read(buf, 0, 4);
            tag = gettagname(buf); //fmt
            if (tag != tagname.FMT) throw new Exception("WaveReadError");
            read = stdout.Read(buf, 0, 4);
            taglen = gettaglen(buf);  //length;
            chunk = new byte[taglen];
            read = stdout.Read(chunk, 0, (int)taglen);
            wavinfo info = parsefmt(chunk);
            info.CloneTo(ref wavinf);

            read = stdout.Read(buf, 0, 4);
            tag = gettagname(buf); //data
            read = stdout.Read(buf, 0, 4);
            taglen = gettaglen(buf);  //length;
            if (tag != tagname.DATA)
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
            byte[] seg = new byte[4];
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
            buff = System.BitConverter.GetBytes((UInt32)(oStream.Length - 0x08));
            oStream.Write(buff, 0, buff.Length);
            oStream.Seek(0x28, SeekOrigin.Begin);
            buff = System.BitConverter.GetBytes((UInt32)(oStream.Length - 0x28));
            oStream.Write(buff, 0, buff.Length);
            oStream.Seek(0, SeekOrigin.Begin);
        }


        enum tagname { RIFF, WAVE, FMT, DATA, Unknown };
        private static tagname gettagname(byte[] seg)
        {
            //[RIFF]
            string segname = System.Text.Encoding.ASCII.GetString(seg, 0, 4);
            tagname tgn = (tagname)Enum.Parse(typeof(tagname), segname.TrimEnd().ToUpper());
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
        public void CloneTo(ref wavinfo CloneTo)
        {
            CloneTo.BitPerSample = BitPerSample;
            CloneTo.BlockAlign = BlockAlign;
            CloneTo.ByteRate = ByteRate;
            CloneTo.Channels = Channels;
            CloneTo.FormatTag = FormatTag;
            CloneTo.Frequency = Frequency;
        }

    }


    class WSOLA
    {
        [DllImport("wsolalib.dll")]
        public static extern void setWSOLAPara(double Hanning_Duration, double Hanning_Overlap, double Delta_Divisor, double Sample_Frequency);

        [DllImport("wsolalib.dll")]
        public static extern void initWSOLA(int channels, int frequency, double tau);

        [DllImport("wsolalib.dll")]
        public static extern int getOutputSize();

        [DllImport("wsolalib.dll")]
        public static extern int getInputSize();

        [DllImport("wsolalib.dll")]
        public static extern void initProcess([MarshalAs(UnmanagedType.LPArray)]Byte[] input);

        [DllImport("wsolalib.dll")]
        public static extern void prereadSrc(ref int srcIndex, ref int srcLen);

        [DllImport("wsolalib.dll")]
        public static extern void prereadDes(ref int desIndex, ref int desLen);

        [DllImport("wsolalib.dll")]
        public static extern void loadSource([MarshalAs(UnmanagedType.LPArray)]Byte[] input);

        [DllImport("wsolalib.dll")]
        public static extern void loadDesire([MarshalAs(UnmanagedType.LPArray)]Byte[] input);

        [DllImport("wsolalib.dll")]
        public static extern void process([MarshalAs(UnmanagedType.LPArray)]Byte[] output);

    }

}
