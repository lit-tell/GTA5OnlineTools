using System;
using System.Linq;

namespace GTA5OnlineTools.Features.Core
{
    public class NopHandle
    {
        private static readonly byte NopCode = 0x90;

        public long StartAddress { get; protected set; }
        public int Length { get; set; }
        public byte[] Original { get; set; }
        public bool IsNoped { get; protected set; }

        public NopHandle(long? startAddress, int? length)
        {
            StartAddress = startAddress ?? throw new ArgumentNullException(nameof(startAddress));
            Length = length ?? throw new ArgumentNullException(nameof(length));
            Original = Memory.ReadBytes(StartAddress, Length);
        }

        public void Nop()
        {
            Memory.WriteBytes(StartAddress, Enumerable.Repeat<byte>(NopCode, Length).ToArray());
            IsNoped = true;
        }

        public void ReStore()
        {
            Memory.WriteBytes(StartAddress, Original);
            IsNoped = false;
        }

        public override string ToString() => $"{IsNoped}";

        ~NopHandle()
        {
            ReStore();
        }
    }
}
