using System;
using Fuyu.Compression;

namespace Elskom.Generic.Libs
{
    /// <summary>
    /// The zlib stream class.
    /// </summary>
    public sealed class ZStream
    {
        private const int MAXWBITS = 15; // 32K LZ77 window

        // private const int ZNOFLUSH = 0;
        // private const int ZPARTIALFLUSH = 1;
        // private const int ZSYNCFLUSH = 2;
        // private const int ZFULLFLUSH = 3;
        // private const int ZFINISH = 4;
        // private const int MAXMEMLEVEL = 9;
        // private const int ZOK = 0;
        // private const int ZSTREAMEND = 1;
        // private const int ZNEEDDICT = 2;
        // private const int ZERRNO = -1;
        private const int ZSTREAMERROR = -2;

        // private const int ZDATAERROR = -3;
        // private const int ZMEMERROR = -4;
        // private const int ZBUFERROR = -5;
        // private const int ZVERSIONERROR = -6;
        private const int DEFWBITS = MAXWBITS;

        /// <summary>
        /// Gets or sets the next input byte.
        /// </summary>
        internal byte[] NextIn { get; set; }

        /// <summary>
        /// Gets or sets the next input byte index.
        /// </summary>
        internal int NextInIndex { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes available at next_in.
        /// </summary>
        internal int AvailIn { get; set; }

        /// <summary>
        /// Gets or sets the total number of input bytes read so far.
        /// </summary>
        internal long TotalIn { get; set; }

        /// <summary>
        /// Gets or sets the next output byte.
        /// </summary>
        internal byte[] NextOut { get; set; }

        /// <summary>
        /// Gets or sets the next output byte index.
        /// </summary>
        internal int NextOutIndex { get; set; }

        /// <summary>
        /// Gets or sets the remaining free space at next_out.
        /// </summary>
        internal int AvailOut { get; set; }

        /// <summary>
        /// Gets or sets the total number of bytes output so far.
        /// </summary>
        internal long TotalOut { get; set; }

        /// <summary>
        /// Gets or sets the stream's error message.
        /// </summary>
        internal string Msg { get; set; }

        /// <summary>
        /// Gets or sets the Stream Data's Adler32 checksum.
        /// </summary>
        internal long Adler { get; set; }

        /// <summary>
        /// Gets the current Deflate instance for this class.
        /// </summary>
        internal Deflate Dstate { get; set; }

        /// <summary>
        /// Gets the current Inflate instance for this class.
        /// </summary>
        internal Inflate Istate { get; private set; }

        /// <summary>
        /// Gets or sets the data type to this instance of this class.
        /// </summary>
        internal int DataType { get; set; } // best guess about the data type: ascii or binary

        /// <summary>
        /// Initializes decompression.
        /// </summary>
        /// <returns>The state.</returns>
        internal int InflateInit() => this.InflateInit(DEFWBITS);

        /// <summary>
        /// Initializes decompression.
        /// </summary>
        /// <param name="w">The window size.</param>
        /// <returns>The zlib status state.</returns>
        internal int InflateInit(int w)
        {
            this.Istate = new Inflate();
            return this.Istate.InflateInit(this, w);
        }

        /// <summary>
        /// Decompresses data.
        /// </summary>
        /// <param name="f">The flush mode to use.</param>
        /// <returns>The zlib status state.</returns>
        internal int Inflate(int f) => this.Istate == null ? ZSTREAMERROR : Libs.Inflate.Decompress(this, f);

        /// <summary>
        /// Ends decompression.
        /// </summary>
        /// <returns>The zlib status state.</returns>
        internal int InflateEnd()
        {
            if (this.Istate == null)
            {
                return ZSTREAMERROR;
            }

            var ret = this.Istate.InflateEnd(this);
            this.Istate = null;
            return ret;
        }

        /// <summary>
        /// Syncs inflate.
        /// </summary>
        /// <returns>The zlib status state.</returns>
        internal int InflateSync() => this.Istate == null ? ZSTREAMERROR : Libs.Inflate.InflateSync(this);

        /// <summary>
        /// Sets the inflate dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to use.</param>
        /// <param name="dictLength">The dictionary length.</param>
        /// <returns>The zlib status state.</returns>
        internal int InflateSetDictionary(Span<byte> dictionary, int dictLength) => this.Istate == null ? ZSTREAMERROR : Libs.Inflate.InflateSetDictionary(this, dictionary, dictLength);

        /// <summary>
        /// Initializes compression.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <returns>The zlib status state.</returns>
        internal int DeflateInit(CompressionLevel level) => this.DeflateInit(level, MAXWBITS);

        /// <summary>
        /// Initializes compression.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bits">The window bits to use.</param>
        /// <returns>The zlib status state.</returns>
        internal int DeflateInit(CompressionLevel level, int bits)
        {
            this.Dstate = new Deflate();
            return this.Dstate.DeflateInit(this, level, bits);
        }

        /// <summary>
        /// Compress data.
        /// </summary>
        /// <param name="flush">The flush mode to use on the data.</param>
        /// <returns>The zlib status state.</returns>
        internal int Deflate(int flush) => this.Dstate == null ? ZSTREAMERROR : this.Dstate.Compress(this, flush);

        /// <summary>
        /// Ends compression.
        /// </summary>
        /// <returns>The zlib status state.</returns>
        internal int DeflateEnd()
        {
            if (this.Dstate == null)
            {
                return ZSTREAMERROR;
            }

            var ret = this.Dstate.DeflateEnd();
            this.Dstate = null;
            return ret;
        }

        /// <summary>
        /// Sets the compression paramiters.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="strategy">The strategy to use for compression.</param>
        /// <returns>The zlib status state.</returns>
        internal int DeflateParams(CompressionLevel level, CompressionStrategy strategy) => this.Dstate == null ? ZSTREAMERROR : this.Dstate.DeflateParams(this, level, strategy);

        /// <summary>
        /// Sets the deflate dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to use.</param>
        /// <param name="dictLength">The dictionary length.</param>
        /// <returns>The zlib status state.</returns>
        internal int DeflateSetDictionary(ReadOnlySpan<byte> dictionary, int dictLength) => this.Dstate == null ? ZSTREAMERROR : this.Dstate.DeflateSetDictionary(this, dictionary.ToArray(), dictLength);

        /// <summary>
        /// Frees everything.
        /// </summary>
        internal void Free()
        {
            this.NextIn = null;
            this.NextOut = null;
            this.Msg = null;
        }

        // Flush as much pending output as possible. All deflate() output goes
        // through this function so some applications may wish to modify it
        // to avoid allocating a large strm->next_out buffer and copying into it.
        // (See also read_buf()).
        internal void Flush_pending()
        {
            var len = this.Dstate.Pending;

            if (len > this.AvailOut)
            {
                len = this.AvailOut;
            }

            if (len == 0)
            {
                return;
            }

            if (this.Dstate.PendingBuf.Length <= this.Dstate.PendingOut || this.NextOut.Length <= this.NextOutIndex || this.Dstate.PendingBuf.Length < (this.Dstate.PendingOut + len) || this.NextOut.Length < (this.NextOutIndex + len))
            {
                // System.Console.Out.WriteLine(dstate.pending_buf.Length + ", " + dstate.pending_out + ", " + next_out.Length + ", " + next_out_index + ", " + len);
                // System.Console.Out.WriteLine("avail_out=" + avail_out);
            }

            Array.Copy(this.Dstate.PendingBuf, this.Dstate.PendingOut, this.NextOut, this.NextOutIndex, len);

            this.NextOutIndex += len;
            this.Dstate.PendingOut += len;
            this.TotalOut += len;
            this.AvailOut -= len;
            this.Dstate.Pending -= len;
            if (this.Dstate.Pending == 0)
            {
                this.Dstate.PendingOut = 0;
            }
        }

        // Read a new buffer from the current input stream, update the adler32
        // and total number of bytes read.  All deflate() input goes through
        // this function so some applications may wish to modify it to avoid
        // allocating a large strm->next_in buffer and copying from it.
        // (See also flush_pending()).
        internal int Read_buf(byte[] buf, int start, int size)
        {
            var len = this.AvailIn;

            if (len > size)
            {
                len = size;
            }

            if (len == 0)
            {
                return 0;
            }

            this.AvailIn -= len;

            if (this.Dstate.Noheader == 0)
            {
                this.Adler = Adler32.Calculate(this.Adler, this.NextIn, this.NextInIndex, len);
            }

            Array.Copy(this.NextIn, this.NextInIndex, buf, start, len);
            this.NextInIndex += len;
            this.TotalIn += len;
            return len;
        }
    }
}