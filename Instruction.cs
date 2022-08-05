namespace SnesEditing
{
	using System;


	public struct Instruction
	{
		public readonly Op Op;
		public readonly bool IsLittleEndian;
		public readonly byte[] OperandBytes;
		public readonly byte[] DirectBytesInsert;


		public Instruction(byte[] directBytesInsert)
		{
			this.DirectBytesInsert = directBytesInsert;
			this.Op = default(Op);
			this.IsLittleEndian = true;
			this.OperandBytes = null;
		}


		public Instruction(int bytesToCopy)
		{
			this.Op = default(Op);
			this.IsLittleEndian = true;
			this.OperandBytes = null;
			this.DirectBytesInsert = null;
		}


		public Instruction(Op op, params byte[] operandBytes)
		{
			this.Op = op;
			int requiredOperands = op.Length - 1;
			if (requiredOperands > 0
				&& operandBytes == null)
			{
				throw new ArgumentNullException($"Opcode must have {requiredOperands} operand bytes.");
			}

			if (operandBytes.Length != requiredOperands)
				throw new ArgumentOutOfRangeException($"Opcode must have {requiredOperands} operand bytes.");

			this.OperandBytes = operandBytes;
			this.IsLittleEndian = true;
			this.DirectBytesInsert = null;
		}


		public Instruction(Op op, bool isLittleEndian, params byte[] operandBytes)
			: this(op, operandBytes)
		{
			this.IsLittleEndian = isLittleEndian;
		}


		/// <summary>
		/// Length of instruction in bytes.
		/// </summary>
		public int Length
		{
			get
			{
				if (this.DirectBytesInsert != null)
					return this.DirectBytesInsert.Length;

				return this.Op.Length;
			}
		}


		/// <summary>
		/// Convert instruction to bytes than may be written.
		/// </summary>
		/// <returns>Concatenated bytes in correct order.</returns>
		public byte[] GetBytes()
		{
			if (this.DirectBytesInsert != null)
				return this.DirectBytesInsert;

			byte[] bytes = new byte[this.Op.Length];
			bytes[0] = this.Op.Hex;

			int requiredOperands = this.Op.Length - 1;

			if (requiredOperands > 0)
			{
				byte[] operandBytes = new byte[this.OperandBytes.Length];
				Array.Copy(this.OperandBytes, operandBytes, this.OperandBytes.Length);

				for (int i = 0; i < requiredOperands; i++)
				{
					bytes[i + 1] = operandBytes[i];
				}
			}

			return bytes;
		}
	}
}