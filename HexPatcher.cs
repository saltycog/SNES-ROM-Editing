namespace SnesEditing
{
	using System;
	using System.Collections.Generic;


	public abstract class HexPatcher
	{
		protected IDictionary<uint, IList<Instruction>> buffer;
		protected IDictionary<byte, byte> bankMap;
		protected uint currentOffset;
		protected byte[] copiedBytes;
		protected bool logToConsole = true;


		#region Constructors
		protected HexPatcher(bool logToConsole = true)
		{
			this.bankMap = new Dictionary<byte, byte>();
			this.buffer = new Dictionary<uint, IList<Instruction>>();
			this.currentOffset = 0x00000000;
			this.logToConsole = logToConsole;
		}
		#endregion


		public static void SetShort(byte[] data, ushort value, int offset)
		{
			byte[] valueBytes = GetShortBytes(value);
			data[offset] = valueBytes[0];
			data[offset + 1] = valueBytes[1];
		}

		public static ushort GetShort(byte[] data, int offset)
		{
			ushort shortAtOffset = (ushort)(data[offset + 1] << 8);
			shortAtOffset += data[offset];

			return shortAtOffset;
		}


		public static byte[] GetShortBytes(ushort value)
		{
			return
				new[]
					{
						(byte)value,
						(byte)(value >> 8)
					};
		}


		public virtual void Apply(byte[] bytesToEdit)
		{
			LogLine("Queued Edits:");
			foreach (KeyValuePair<uint, IList<Instruction>> entry in this.buffer)
			{
				uint offset = entry.Key;
				byte[] bytesToWrite = GetBytesForInstructions(offset);

				// Remap offset if needed.
				byte[] offsetHexBytes = BitConverter.GetBytes(offset);
				Array.Reverse(offsetHexBytes); // Little Endian reverses operand bytes.
				if (this.bankMap.ContainsKey(offsetHexBytes[1]))
				{
					offsetHexBytes[1] = this.bankMap[offsetHexBytes[1]];
					Array.Reverse(offsetHexBytes); // Little Endian reverses operand bytes.
					offset = BitConverter.ToUInt32(offsetHexBytes, 0);
				}

				LogLine($"{offset:X8} -->");

				uint offsetColumn = offset % 16;
				int currentColumn = 0;
				for (int i = 0; i < offsetColumn; i++)
				{
					if (currentColumn == 15)
						LogLine("xx");
					else if (i == 7)
						Log("xx - ");
					else
						Log("xx ");
					currentColumn++;
				}

				for (int i = 0; i < bytesToWrite.Length; i++)
				{
					offsetColumn++;
					long writeOffset = offset + i;
					Log($"{bytesToWrite[i]:X2}");
					bytesToEdit[writeOffset] = bytesToWrite[i];

					// Additional formatting for console output based on byte position.
					if (offsetColumn % 16 == 0)
						LogLine();
					else if (offsetColumn % 8 == 0)
						Log(" - ");
					else
						Log(" ");
				}

				uint remainingBytes = 16 - (offsetColumn % 16);
				for (uint i = 0; i < remainingBytes; i++)
				{
					offsetColumn++;
					if (offsetColumn / 8f == 1)
						Log("xx - ");
					else
						Log("xx ");
				}

				LogLine();
				LogLine();
			}
		}


		/// <summary>
		/// Start editing at given offset.
		/// </summary>
		/// <param name="offset">Offset where new edits will occur.</param>
		public void ChangeOffset(uint offset)
		{
			this.currentOffset = offset;
		}


		public void ClearBuffer()
		{
			this.buffer.Clear();
			this.buffer = new Dictionary<uint, IList<Instruction>>();
		}


		public void MapBank(byte bank, byte mapping)
		{
			if (!this.bankMap.ContainsKey(bank))
				this.bankMap.Add(bank, mapping);
		}


		public void MapBankRange(byte startingBank, byte endingBank, byte mappingStart)
		{
			byte range = (byte)(endingBank - startingBank);
			for (byte i = 0; i <= range; i++)
			{
				MapBank((byte)(startingBank + i), (byte)(mappingStart + i));
			}
		}


		protected void AddInstruction(Instruction instruction)
		{
			if (!this.buffer.ContainsKey(this.currentOffset))
				this.buffer.Add(this.currentOffset, new List<Instruction>());

			this.buffer[this.currentOffset].Add(instruction);
		}


		protected void AddInstruction(Op op, byte operand)
		{
			AddInstruction(new Instruction(op, operand));
		}


		protected void AddInstruction(Op op, params byte[] operands)
		{
			AddInstruction(new Instruction(op, operands));
		}


		protected void AddInstruction(Op op, ushort address)
		{
			byte[] addressBytes = BitConverter.GetBytes(address);
			AddInstruction(new Instruction(op, addressBytes));
		}


		protected void AddInstruction(Op op, uint address)
		{
			byte[] addressBytes = BitConverter.GetBytes(address);
			AddInstruction(
				new Instruction(
					op,
					addressBytes[0],
					addressBytes[1],
					addressBytes[2]));
		}


		protected void AddInstruction(Op op)
		{
			AddInstruction(new Instruction(op));
		}


		protected int GetBufferLength(uint offset)
		{
			if (!this.buffer.ContainsKey(offset))
				return 0;

			int length = 0;
			IList<Instruction> entry = this.buffer[offset];
			for (int i = 0; i < entry.Count; i++)
			{
				length += entry[i].Length;
			}

			return length;
		}


		protected byte[] GetBytesAtOffset(uint offset, int numberOfBytes, byte[] byteArray)
		{
			// Remap offset if needed.
			byte[] offsetHexBytes = BitConverter.GetBytes(offset);
			Array.Reverse(offsetHexBytes); // Little Endian reverses operand bytes.
			if (this.bankMap.ContainsKey(offsetHexBytes[1]))
			{
				offsetHexBytes[1] = this.bankMap[offsetHexBytes[1]];
				Array.Reverse(offsetHexBytes); // Little Endian reverses operand bytes.
				offset = BitConverter.ToUInt32(offsetHexBytes, 0);
			}

			byte[] copiedBytes = new byte[numberOfBytes];
			for (int i = 0; i < numberOfBytes; i++)
			{
				copiedBytes[i] = byteArray[offset + i];
			}

			return copiedBytes;
		}


		protected byte[] GetBytesForInstructions(uint offset)
		{
			if (!this.buffer.ContainsKey(offset))
				throw new KeyNotFoundException($"No such offset in HexPatcher buffer: {offset}.");

			byte[] bytesForInstructions = new byte[GetBufferLength(offset)];

			IList<Instruction> instructions = this.buffer[offset];

			int index = 0;
			foreach (Instruction instruction in instructions)
			{
				byte[] instructionBytes = instruction.GetBytes();

				Buffer.BlockCopy(
					instructionBytes,
					0,
					bytesForInstructions,
					index,
					instructionBytes.Length);
				index += instructionBytes.Length;
			}

			return bytesForInstructions;
		}


		protected void Log(string text)
		{
			if (this.logToConsole)
			{
				Console.Write(text);
			}
		}


		protected void LogLine(string text = "")
		{
			if (this.logToConsole)
			{
				Console.WriteLine(text);
			}
		}
	}
}