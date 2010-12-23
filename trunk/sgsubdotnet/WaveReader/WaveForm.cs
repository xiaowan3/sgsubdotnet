using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WaveReader
{
    public class WaveForm
    {
        /// <summary>
        /// FFMPEG位置
        /// </summary>
        public static string FFmpegpath { get; set; }

        
        /// <summary>
        /// 长度
        /// </summary>
        public double Length { get; private set; }

        public double DeltaT = 0;

        /// <summary>
        /// 抽取音频波形
        /// </summary>
        /// <param name="videofilename">文件名</param>
        /// <returns>Wave</returns>
        public static WaveForm ExtractWave(string videofilename)
        {
            //ffmpeg -i <infile> -f wav -ac 1 -vn -y <outfile.wav>
            IntPtr fftbuf = fft.CreateFFTBuffer(960);
            WaveForm wavfm = new WaveForm();
            List<Byte[][]> wflist = new List<Byte[][]>();
            Byte[][] asec;
            Process ffmpegprocess = new Process();
            ffmpegprocess.StartInfo.FileName = FFmpegpath;
            ffmpegprocess.StartInfo.Arguments = "-i \"" + videofilename + "\" -f wav -ac 1 -vn -y -";
            ffmpegprocess.StartInfo.CreateNoWindow = true;
            ffmpegprocess.StartInfo.RedirectStandardOutput = true;
            ffmpegprocess.StartInfo.UseShellExecute = false;
            ffmpegprocess.Start();
            Stream stdout = ffmpegprocess.StandardOutput.BaseStream;
            byte[] buf;
            byte[] chunk;
            wavinfo wavinf;
            //get wavinfo
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
            wavinf = parsefmt(chunk);

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

            int samplelen = (int)(wavinf.Frequency / 5); //0.1 秒音频的数据
            chunk = new byte[samplelen];
            int timecount = 0;
            int numsplit = 5; //每0.1秒所分的段数
            int spsec = numsplit * 10; //每秒采样点数
            asec = new Byte[spsec][];
            for (int i = 0; i < spsec; i++) asec[i] = new Byte[100];
            
            int[] split = new int[numsplit + 1];
            for (int i = 0; i < numsplit + 1; i++)
                split[i] = (int)(i * (double)(samplelen) / (2 * numsplit)) * 2;
            
            do
            {
                int left,offset = 0;
                do
                {
                    read = stdout.Read(chunk, offset, samplelen-offset);
                    if (read == 0)
                    {
                        for (int i = offset; i < samplelen; i++)
                            chunk[i] = 0;
                        offset = samplelen;
                    }
                    offset += read;
                    left = samplelen - offset;
                } while (left > 0);
                //取每一段(wavfm.DeltaT s)音频的##
                for (int s = 0; s < numsplit; s++)
                {
                    byte[] bb = asec[timecount];
                    //byte[] bb = new byte[100];
                    fft.DoFFT(fftbuf, chunk, (split[s + 1] - split[s])/2, split[s]/2, bb);
                    timecount++;
                }


                if (timecount >= spsec)
                {
                    wflist.Add(asec);
                    asec = new Byte[spsec][];
                    for (int i = 0; i < spsec; i++) asec[i] = new Byte[100];
                    timecount = 0;
                }

            } while (read > 0);
            //保存波形信息
            wavfm.m_waveform = new byte[wflist.Count * spsec][];

            wavfm.DeltaT = 0.1 / numsplit;
            for (int i = 0; i < wflist.Count; i++)
            {
                for (int j = 0; j < spsec; j++)
                    wavfm.m_waveform[i * spsec + j] = wflist[i][j];
            }
            wavfm.Length = wavfm.m_waveform.Length * wavfm.DeltaT;


            return wavfm;
        }

        enum tagname{RIFF,WAVE,FMT,DATA,Unknown};
        private static tagname gettagname(byte[] seg)
        {
            //[RIFF]
            if (seg[0] == 0x52 && seg[1] == 0x49 && seg[2] == 0x46 && seg[3] == 0x46)
            {

                return tagname.RIFF;
            }
            //[WAVE]
            else if (seg[0] == 0x57 && seg[1] == 0x41 && seg[2] == 0x56 && seg[3] == 0x45)
            {

                return tagname.WAVE;
            }
            //[fmt ]
            else if (seg[0] == 0x66 && seg[1] == 0x6D && seg[2] == 0x74)
            {

                return tagname.FMT;
            }
            //[data]
            else if (seg[0] == 0x64 && seg[1] == 0x61 && seg[2] == 0x74 && seg[3] == 0x61)
            {

                return tagname.DATA;
            }
            else
            {
                return tagname.Unknown;
            }
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


        /// <summary>
        /// 读取当前时间的响度
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>响度值</returns>
        public Byte[] ValueAt(double time)
        {
            int l = (int)(time / DeltaT);
            if (l < 0 || l >= m_waveform.Length) return m_nullwave;
            return m_waveform[l];
        }
        private Byte[][] m_waveform;
        private Byte[] m_nullwave = new Byte[100];
    }
    class wavinfo
    {
        public UInt32 FormatTag = 0;
        public UInt32 Channels = 0;
        public UInt32 Frequency = 0;
        public UInt32 BitPerSample = 0;
        public UInt32 ByteRate = 0;
        public UInt32 BlockAlign = 0;

    }
    class fft
    {
        [DllImport("fftsupport.dll")]
        public static extern IntPtr CreateFFTBuffer(Int32 len);

        [DllImport("fftsupport.dll")]
        public static extern void DoFFT(IntPtr fftbuf, [MarshalAs(UnmanagedType.LPArray)]Byte[] input, int inlen, int inoffset, [MarshalAs(UnmanagedType.LPArray)] Byte[] output);
    }
}
