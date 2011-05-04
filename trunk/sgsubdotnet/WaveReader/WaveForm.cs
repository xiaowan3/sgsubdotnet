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
            var wavfm = new WaveForm();
            var wflist = new List<Byte[][]>();
            var ffmpegprocess = new Process
                                    {
                                        StartInfo =
                                            {
                                                FileName = FFmpegpath,
                                                Arguments =
                                                    "-i \"" + videofilename + "\" -f wav -ac 1 -ar 48000 -vn -y -",
                                                CreateNoWindow = true,
                                                RedirectStandardOutput = true,
                                                UseShellExecute = false
                                            }
                                    };
            ffmpegprocess.Start();
            Stream stdout = ffmpegprocess.StandardOutput.BaseStream;
            //get wavinfo
            byte[] buf = new byte[4];
            
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
            byte[] chunk = new byte[taglen];
            read = stdout.Read(chunk, 0, (int)taglen);
            Wavinfo wavinf = Parsefmt(chunk);

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
            if (wavinf.Frequency != 48000)
            {
                throw new Exception("Please report bug: Unsupported Frequency");
            }

            int samplelen = (int)(wavinf.Frequency / 5); //0.1 秒音频的数据(字节数，所以是除以5)
            chunk = new byte[samplelen];
            int timecount = 0;
            int numsplit = 5; //每0.1秒所分的段数
            int spsec = numsplit * 10; //每秒数据点数
            int nSamples = samplelen / 10;//每个FFT周期的采样个数
            IntPtr fftbuf = FFT.CreateFFTBuffer(nSamples);

            var asec = new Byte[spsec][];
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
                    FFT.DoFFT(fftbuf, chunk, (split[s + 1] - split[s]) / 2, split[s] / 2, asec[timecount]);
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
            wavfm._mWaveform = new byte[wflist.Count * spsec][];

            wavfm.DeltaT = 0.1 / numsplit;
            for (int i = 0; i < wflist.Count; i++)
            {
                for (int j = 0; j < spsec; j++)
                    wavfm._mWaveform[i * spsec + j] = wflist[i][j];
            }
            wavfm.Length = wavfm._mWaveform.Length * wavfm.DeltaT;


            return wavfm;
        }

        enum Tagname{RIFF,WAVE,FMT,DATA,Unknown};
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
                             Frequency =BitConverter.ToUInt32(chunk,4),
                             ByteRate =BitConverter.ToUInt32(chunk,8),
                             BlockAlign = BitConverter.ToUInt16(chunk, 12),
                             BitPerSample = BitConverter.ToUInt16(chunk, 14)
                         };
            return wi;
        }


        /// <summary>
        /// 读取当前时间的频谱
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>频谱</returns>
        public Byte[] ValueAt(double time)
        {
            int l = (int)(time / DeltaT);
            if (l < 0 || l >= _mWaveform.Length) return _nullwave;
            return _mWaveform[l];
        }
        private Byte[][] _mWaveform;
        private Byte[] _nullwave = new Byte[100];
    }
    class Wavinfo
    {
        public UInt32 FormatTag = 0;
        public UInt32 Channels = 0;
        public UInt32 Frequency = 0;
        public UInt32 BitPerSample = 0;
        public UInt32 ByteRate = 0;
        public UInt32 BlockAlign = 0;

    }

    static class FFT
    {
        [DllImport("fftsupport.dll")]
        public static extern IntPtr CreateFFTBuffer(Int32 len);

        [DllImport("fftsupport.dll")]
        public static extern void DoFFT(IntPtr fftbuf, [MarshalAs(UnmanagedType.LPArray)]Byte[] input, int inlen, int inoffset, [MarshalAs(UnmanagedType.LPArray)] Byte[] output);
    }
}
