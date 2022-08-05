namespace SnesEditing
{
	using System;


	public abstract class Asm65816Patcher<T> : HexPatcher
		where T : Asm65816Patcher<T>
	{
		public static readonly Op AdcAddr = new Op("ADC", 0x6D, 3);
		public static readonly Op AdcAddrX = new Op("ADC", 0x7D, 3);
		public static readonly Op AdcAddrY = new Op("ADC", 0x79, 3);
		public static readonly Op AdcConst = new Op("ADC", 0x69, 2);
		public static readonly Op AdcLong = new Op("ADC", 0x6F, 4);
		public static readonly Op Asl = new Op("ASL", 0x0A, 1);
		public static readonly Op AslAddr = new Op("ASL", 0x0E, 3);
		public static readonly Op AslAddrX = new Op("ASL", 0x1E, 3);
		public static readonly Op BccAddr = new Op("BCC", 0x90, 2);
		public static readonly Op BcsAddr = new Op("BCS", 0xB0, 2);
		public static readonly Op BitAddr = new Op("BIT", 0x2C, 3);
		public static readonly Op BitAddrX = new Op("BIT", 0x3C, 3);
		public static readonly Op BitConst = new Op("BIT", 0x89, 2);
		public static readonly Op BmiAddr = new Op("BMI", 0x30, 2);
		public static readonly Op BneAddr = new Op("BNE", 0xD0, 2);
		public static readonly Op BplAddr = new Op("BPL", 0x10, 2);
		public static readonly Op BvcAddr = new Op("BVC", 0x50, 2);
		public static readonly Op BvsAddr = new Op("BVS", 0x70, 2);
		public static readonly Op Clc = new Op("CLC", 0x18, 1);
		public static readonly Op CmpAddr = new Op("CMP", 0xCD, 3);
		public static readonly Op CmpAddrX = new Op("CMP", 0xDD, 3);
		public static readonly Op CmpAddrY = new Op("CMP", 0xD9, 3);
		public static readonly Op CmpConst = new Op("CMP", 0xC9, 2);
		public static readonly Op CmpConst16 = new Op("CMP", 0xC9, 3);
		public static readonly Op CmpLong = new Op("CMP", 0xCF, 4);
		public static readonly Op CmpLongX = new Op("CMP", 0xDF, 4);
		public static readonly Op CpxAddr = new Op("CPX", 0xEC, 3);
		public static readonly Op CpxConst = new Op("CPX", 0xE0, 2);
		public static readonly Op CpxConst16 = new Op("CPX", 0xE0, 3);
		public static readonly Op CpyAddr = new Op("CPY", 0xCC, 3);
		public static readonly Op CpyConst = new Op("CPY", 0xC0, 3);
		public static readonly Op Dec = new Op("DEC", 0x3A, 1);
		public static readonly Op DecAddr = new Op("DEC", 0xCE, 3);
		public static readonly Op DecAddrX = new Op("DEC", 0xDE, 3);
		public static readonly Op Dex = new Op("DEX", 0xCA, 1);
		public static readonly Op Dey = new Op("DEY", 0x88, 1);
		public static readonly Op Inc = new Op("INC", 0x1A, 1);
		public static readonly Op IncAddr = new Op("INC", 0xEE, 3);
		public static readonly Op IncAddrX = new Op("INC", 0xFE, 3);
		public static readonly Op IncDp = new Op("INC", 0xE6, 2);
		public static readonly Op Inx = new Op("INX", 0xE8, 1);
		public static readonly Op Iny = new Op("INY", 0xC8, 1);
		public static readonly Op JmpAddr = new Op("JMP", 0x4C, 3);
		public static readonly Op JmpAddrX = new Op("JMP", 0x7C, 3);
		public static readonly Op JmpLong = new Op("JMP", 0x5C, 4);
		public static readonly Op Jsl = new Op("JSL", 0x22, 4);
		public static readonly Op JsrAddr = new Op("JSR", 0x20, 3);
		public static readonly Op LdaAddr = new Op("LDA", 0xAD, 3);
		public static readonly Op LdaAddrX = new Op("LDA", 0xBD, 3);
		public static readonly Op LdaAddrY = new Op("LDA", 0xB9, 3);
		public static readonly Op LdaConst = new Op("LDA", 0xA9, 2);
		public static readonly Op LdaDp = new Op("LDA", 0xA5, 2);
		public static readonly Op LdaLong = new Op("LDA", 0xAF, 4);
		public static readonly Op LdaLongX = new Op("LDA", 0xBF, 4);
		public static readonly Op LdxAddr = new Op("LDX", 0xAE, 3);
		public static readonly Op LdxAddrY = new Op("LDX", 0xBE, 3);
		public static readonly Op LdxConst = new Op("LDX", 0xA2, 2);
		public static readonly Op LdxConst16 = new Op("LDX", 0xA2, 3);
		public static readonly Op LdyAddr = new Op("LDY", 0xAC, 3);
		public static readonly Op LdyAddrX = new Op("LDY", 0xBC, 3);
		public static readonly Op LdyConst = new Op("LDY", 0xA0, 2);
		public static readonly Op LdyDp = new Op("LDY", 0xA4, 2);
		public static readonly Op Lsr = new Op("LSR", 0x4A, 1);
		public static readonly Op LsrAddr = new Op("LSR", 0x4E, 3);
		public static readonly Op LsrAddrX = new Op("LSR", 0x5E, 3);
		public static readonly Op Nop = new Op("NOP", 0xEA, 1);
		public static readonly Op Pha = new Op("PHA", 0x48, 1);
		public static readonly Op Php = new Op("PHP", 0x08, 1);
		public static readonly Op Phx = new Op("PHX", 0xDA, 1);
		public static readonly Op Phy = new Op("PHY", 0x5A, 1);
		public static readonly Op Pla = new Op("PLA", 0x68, 1);
		public static readonly Op Plp = new Op("PLP", 0x28, 1);
		public static readonly Op Plx = new Op("PLX", 0xFA, 1);
		public static readonly Op Ply = new Op("PLY", 0x7A, 1);
		public static readonly Op RepConst = new Op("REP", 0xC2, 2);
		public static readonly Op Rtl = new Op("RTL", 0x6B, 1);
		public static readonly Op Rts = new Op("RTS", 0x60, 1);
		public static readonly Op SbcAddr = new Op("SBC", 0xED, 3);
		public static readonly Op SbcAddrX = new Op("SBC", 0xFD, 3);
		public static readonly Op SbcAddrY = new Op("SBC", 0xF9, 3);
		public static readonly Op SbcConst = new Op("SBC", 0xE9, 2);
		public static readonly Op SepConst = new Op("SEP", 0xE2, 2);
		public static readonly Op StaAddr = new Op("STA", 0x8D, 3);
		public static readonly Op StaAddrX = new Op("STA", 0x9D, 3);
		public static readonly Op StaAddrY = new Op("STA", 0x99, 3);
		public static readonly Op StaDp = new Op("STA", 0x85, 2);
		public static readonly Op StaLong = new Op("STA", 0x8F, 4);
		public static readonly Op StaLongX = new Op("STA", 0x9F, 4);
		public static readonly Op Tax = new Op("TAX", 0xAA, 1);
		public static readonly Op Tay = new Op("TAY", 0xA8, 1);
		public static readonly Op Tcd = new Op("TCD", 0x5B, 1);
		public static readonly Op Tdc = new Op("TDC", 0x7B, 1);
		public static readonly Op Txa = new Op("TXA", 0x8A, 1);
		public static readonly Op Txy = new Op("TXY", 0x9B, 1);
		public static readonly Op Tya = new Op("TYA", 0x98, 1);
		public static readonly Op Tyx = new Op("TYX", 0xBB, 1);
		public static readonly Op Wdm = new Op("WDM", 0x42, 2);


		/// <summary>
		/// Add a numeric value to the Accumulator.
		/// Bytes: 2 (3 if 16-bit Accumulator).
		/// </summary>
		/// <param name="amount">How much to add.</param>
		public T AddNumber(byte amount)
		{
			AddInstruction(AdcConst, amount);
			return this as T;
		}


		/// <summary>
		/// Add a numeric value from a given address in the same bank to the Accumulator.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to get value from.</param>
		public T AddAddressValue(ushort address)
		{
			AddInstruction(AdcAddr, address);
			return this as T;
		}


		/// <summary>
		/// Add a numeric value from a given address in same bank, at index X, to the Accumulator.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to get value from.</param>
		public T AddAddressValueAtIndexX(ushort address)
		{
			AddInstruction(AdcAddrX, address);
			return this as T;
		}


		/// <summary>
		/// Add a numeric value from a given address in same bank, at index Y, to the Accumulator.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to get value from.</param>
		public T AddAddressValueAtIndexY(ushort address)
		{
			AddInstruction(AdcAddrY, address);
			return this as T;
		}


		/// <summary>
		/// Branch given number of bytes if carry flag is set.
		/// Generally used after a comparison.
		/// Distance limit: 126 bytes backward, 129 bytes forward.
		/// Bytes: 2.
		/// </summary>
		/// <param name="byteDistance">Number of bytes to jump.</param>
		public T BranchAcrossBytesIfGreaterThanOrEqual(sbyte byteDistance)
		{
			AddInstruction(BcsAddr, (byte)byteDistance);
			return this as T;
		}


		/// <summary>
		/// Branch given number of bytes if carry flag is clear.
		/// Generally used after a comparison.
		/// Distance limit: 126 bytes backward, 129 bytes forward.
		/// Bytes: 2.
		/// </summary>
		/// <param name="byteDistance">Number of bytes to jump.</param>
		public T BranchAcrossBytesIfLessThan(sbyte byteDistance)
		{
			AddInstruction(BccAddr, (byte)byteDistance);
			return this as T;
		}


		public T BranchAcrossBytesIfNegative(sbyte byteDistance)
		{
			AddInstruction(BmiAddr, (byte)byteDistance);
			return this as T;
		}


		/// <summary>
		/// Branch given number of bytes if zero flag is clear.
		/// Generally used after a comparison.
		/// Computations can also affect the zero flag, however.
		/// Distance limit: 126 bytes backward, 129 bytes forward.
		/// Bytes: 2.
		/// </summary>
		/// <param name="byteDistance">How many bytes to branch forward/backward.</param>
		public T BranchAcrossBytesIfNotEqual(sbyte byteDistance)
		{
			AddInstruction(BneAddr, (byte)byteDistance);
			return this as T;
		}


		public T BranchAcrossBytesIfOverflowClear(sbyte byteDistance)
		{
			AddInstruction(BvcAddr, (byte)byteDistance);
			return this as T;
		}


		public T BranchAcrossBytesIfOverflowSet(sbyte byteDistance)
		{
			AddInstruction(BvsAddr, (byte)byteDistance);
			return this as T;
		}


		public T BranchAcrossBytesIfPositive(sbyte byteDistance)
		{
			AddInstruction(BplAddr, (byte)byteDistance);
			return this as T;
		}


		/// <summary>
		/// Branch to a given address if carry flag is set.
		/// Generally used after a comparison.
		/// Distance limit: 126 bytes backward, 129 bytes forward.
		/// Bytes: 2.
		/// </summary>
		/// <param name="address">Address to branch to.</param>
		public T BranchToAddressIfGreaterThanOrEqual(ushort address)
		{
			AddInstruction(BcsAddr, GetRelativeAddress(address));
			return this as T;
		}


		/// <summary>
		/// Branch to a given address if carry flag is clear.
		/// Generally used after a comparison.
		/// Distance limit: 126 bytes backward, 129 bytes forward.
		/// Bytes: 2.
		/// </summary>
		/// <param name="address">Address to branch to.</param>
		public T BranchToAddressIfLessThan(ushort address)
		{
			AddInstruction(BccAddr, GetRelativeAddress(address));
			return this as T;
		}


		public T BranchToAddressIfNegative(ushort address)
		{
			AddInstruction(BmiAddr, GetRelativeAddress(address));
			return this as T;
		}


		/// <summary>
		/// Branch to a given address if zero flag is clear.
		/// Generally used after a comparison.
		/// Computations can also affect the zero flag, however.
		/// Distance limit: 126 bytes backward, 129 bytes forward.
		/// Bytes: 2.
		/// </summary>
		/// <param name="address">Address to branch to.</param>
		public T BranchToAddressIfNotEqual(ushort address)
		{
			AddInstruction(BneAddr, GetRelativeAddress(address));
			return this as T;
		}


		public T BranchToAddressIfOverflowClear(ushort address)
		{
			AddInstruction(BvcAddr, GetRelativeAddress(address));
			return this as T;
		}


		public T BranchToAddressIfOverflowSet(ushort address)
		{
			AddInstruction(BvsAddr, GetRelativeAddress(address));
			return this as T;
		}


		public T BranchToAddressIfPositive(ushort address)
		{
			AddInstruction(BplAddr, GetRelativeAddress(address));
			return this as T;
		}


		/// <summary>
		/// Performs AND to Accumulator, but only flags are modified.
		/// Use this, and then check the zero flag of Accumulator after,
		/// to check if a particular bit in the Accumulator is 1 or 0.
		/// Bytes: 2.
		/// </summary>
		/// <param name="bit">Bit in Accumulator to check.</param>
		public T CheckBit(byte bit)
		{
			AddInstruction(BitConst, bit);
			return this as T;
		}


		/// <summary>
		/// Clear carry flag from Accumulator.
		/// </summary>
		public T ClearCarry()
		{
			AddInstruction(Clc);
			return this as T;
		}


		/// <summary>
		/// Clears the bits specified on processor status flags.
		/// Generally used to set registers 8 or 16 bits.
		/// Bytes: 2.
		/// </summary>
		/// <param name="value">Value to set </param>
		public T ClearProcessorStatusBits(byte value)
		{
			AddInstruction(RepConst, value);
			return this as T;
		}


		/// <summary>
		/// Compare 8-bit Accumulator value with the given value.
		/// If Accumulator is more than, the n flag is set.
		/// If Accumulator is less than, carry flag is cleared.
		/// If Accumulator is equal to, the zero flag is set.
		/// Bytes: 2.
		/// </summary>
		/// <param name="value">Value to compare to.</param>
		public T Compare(byte value)
		{
			AddInstruction(CmpConst, value);
			return this as T;
		}


		/// <summary>
		/// Compare 16-bit Accumulator value with the given value.
		/// If Accumulator is more than, the n flag is set.
		/// If Accumulator is less than, carry flag is cleared.
		/// If Accumulator is equal to, the zero flag is set.
		/// Bytes: 3.
		/// </summary>
		/// <param name="value">Value to compare to.</param>
		public T Compare16(ushort value)
		{
			AddInstruction(CmpConst16, value);
			return this as T;
		}


		/// <summary>
		/// Compare Accumulator value with the value at given address.
		/// If Accumulator is more than, the n flag is set.
		/// If Accumulator is less than, carry flag is cleared.
		/// If Accumulator is equal to, the zero flag is set.
		/// Bytes: 3.
		/// Distance limit: 128 bytes.
		/// </summary>
		/// <param name="address">Address of value to compare to.</param>
		public T CompareToAddress(ushort address)
		{
			AddInstruction(CmpAddr, address);
			return this as T;
		}


		/// <summary>
		/// Compare Accumulator value with the value at given address in different bank.
		/// If Accumulator is more than, the n flag is set.
		/// If Accumulator is less than, carry flag is cleared.
		/// If Accumulator is equal to, the zero flag is set.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">Address of value to compare to.</param>
		public T CompareToBankAddress(uint address)
		{
			AddInstruction(CmpLong, address);
			return this as T;
		}


		/// <summary>
		/// Compare 8-bit X register value with the given value.
		/// If X is more than, the n flag is set.
		/// If X is less than, carry flag will be cleared.
		/// If X is equal to, the zero flag is set.
		/// Bytes: 2.
		/// </summary>
		/// <param name="value">Byte value to compare to.</param>
		public T CompareX(byte value)
		{
			AddInstruction(CpxConst, value);
			return this as T;
		}


		/// <summary>
		/// Compare 16-bit X register value with the given value.
		/// If X is more than, the n flag is set.
		/// If X is less than, carry flag will be cleared.
		/// If X is equal to, the zero flag is set.
		/// Bytes: 3.
		/// </summary>
		/// <param name="value">Byte value to compare to.</param>
		public T CompareX16(ushort value)
		{
			AddInstruction(CpxConst16, value);
			return this as T;
		}


		/// <summary>
		/// Compare X register value with the value at given address.
		/// If X is more than, the n flag is set.
		/// If X is less than, carry flag is cleared.
		/// If X is equal to, the zero flag is set.
		/// Bytes: 3.
		/// Distance limit: 128 bytes.
		/// </summary>
		/// <param name="address">Address of value to compare to.</param>
		public T CompareXToAddress(ushort address)
		{
			AddInstruction(CpxAddr, address);
			return this as T;
		}


		/// <summary>
		/// Compare Y register value with the given value.
		/// If Y is more than, the n flag is set.
		/// If Y is less than, carry flag will be cleared.
		/// If Y is equal to, the zero flag is set.
		/// Bytes: 2
		/// </summary>
		/// <param name="value">Byte value to compare to.</param>
		public T CompareY(ushort value)
		{
			AddInstruction(CpyConst, value);
			return this as T;
		}


		/// <summary>
		/// Compare Y register value with the value at given address.
		/// If Y is more than, the n flag is set.
		/// If Y is less than, carry flag is cleared.
		/// If Y is equal to, the zero flag is set.
		/// Bytes: 3.
		/// Distance limit: 128 bytes.
		/// </summary>
		/// <param name="address">Address of value to compare to.</param>
		public T CompareYToAddress(ushort address)
		{
			AddInstruction(CpyAddr, address);
			return this as T;
		}


		/// <summary>
		/// Divide Accumulator by 2.
		/// Bytes: 1.
		/// </summary>
		public T DivideBy2()
		{
			AddInstruction(Lsr);
			return this as T;
		}


		/// <summary>
		/// Does nothing, useful for overwriting a single byte.
		/// Note, however, it does take up 2 cycles.
		/// Bytes: 1.
		/// </summary>
		public T DoNothing()
		{
			AddInstruction(Nop);
			return this as T;
		}


		/// <summary>
		/// Does nothing, useful for overwriting two bytes (additionalByte isn't used).
		/// Like single byte version, also takes up 2 cycles, so more efficient.
		/// Bytes: 2.
		/// </summary>
		public T DoNothing(byte additionalByte)
		{
			AddInstruction(Wdm, additionalByte);
			return this as T;
		}


		/// <summary>
		/// Load value from given address into Accumulator.
		/// Distance limit: 128 bytes away.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to get value from.</param>
		public T GetAddressValue(ushort address)
		{
			AddInstruction(LdaAddr, address);
			return this as T;
		}


		/// <summary>
		/// Load value from given address into Accumulator, at index X.
		/// Distance limit: 128 bytes away.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to get value from.</param>
		public T GetAddressValueWithXIndex(ushort address)
		{
			AddInstruction(LdaAddrX, address);
			return this as T;
		}


		/// <summary>
		/// Load value from given address into Accumulator, at index Y.
		/// Distance limit: 128 bytes away.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to get value from.</param>
		public T GetAddressValueWithYIndex(ushort address)
		{
			AddInstruction(LdaAddrY, address);
			return this as T;
		}


		/// <summary>
		/// Load value from given address in a different bank into Accumulator.
		/// Bytes: 4.
		/// </summary>
		/// <param name="address">3 byte address to get value from.</param>
		public T GetBankAddressValue(uint address)
		{
			AddInstruction(LdaLong, address);
			return this as T;
		}


		/// <summary>
		/// Load value from given address in a different bank into Accumulator, at index X.
		/// Bytes: 4.
		/// </summary>
		/// <param name="address">3 byte address to get value from.</param>
		public T GetBankAddressValueWithXIndex(uint address)
		{
			AddInstruction(LdaLongX, address);
			return this as T;
		}


		public int GetBufferLength()
		{
			int length = 0;
			foreach (uint offset in this.buffer.Keys)
			{
				length += GetBufferLength(offset);
			}

			return length;
		}


		/// <summary>
		/// Load Direct Page value at given address.
		/// Bytes: 2.
		/// </summary>
		/// <param name="address">Addres in direct page to get value of.</param>
		public T GetDirectPage(byte address)
		{
			AddInstruction(LdaDp, address);
			return this as T;
		}


		/// <summary>
		/// Load Direct Page register into Accumulator.
		/// If Direct Page is 0, clears out Accumulator.
		/// Bytes: 1.
		/// </summary>
		public T GetDirectPage()
		{
			AddInstruction(Tdc);
			return this as T;
		}


		/// <summary>
		/// Load X register into Accumulator.
		/// Bytes: 1.
		/// </summary>
		public T GetX()
		{
			AddInstruction(Txa);
			return this as T;
		}


		/// <summary>
		/// Load Y register into Accumulator.
		/// Bytes: 1.
		/// </summary>
		/// <returns></returns>
		public T GetY()
		{
			AddInstruction(Tya);
			return this as T;
		}


		/// <summary>
		/// Increment value in direct page address by 1.
		/// Bytes: 2.
		/// </summary>
		/// <param name="address">Address in direct page to increment.</param>
		public T IncrementDirectPage(byte address)
		{
			AddInstruction(IncDp, address);
			return this as T;
		}


		/// <summary>
		/// Increment Accumulator by 1.
		/// Bytes: 1.
		/// </summary>
		public T Increment()
		{
			AddInstruction(Inc);
			return this as T;
		}


		/// <summary>
		/// Increment value in X register by 1.
		/// Bytes: 1.
		/// </summary>
		public T IncrementX()
		{
			AddInstruction(Inx);
			return this as T;
		}


		/// <summary>
		/// Increment value in Y register by 1.
		/// Bytes: 1.
		/// </summary>
		public T IncrementY()
		{
			AddInstruction(Iny);
			return this as T;
		}


		/// <summary>
		/// Jump to a subroutine at the given address in a different bank.
		/// Bytes: 4.
		/// </summary>
		/// <param name="address">3 byte address to jump to.</param>
		public T JumpToBankSubroutine(uint address)
		{
			AddInstruction(Jsl, address);
			return this as T;
		}


		/// <summary>
		/// Jump to a subroutine at the given address, up to 128 bytes away.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to jump to.</param>
		public T JumpToSubroutine(ushort address)
		{
			AddInstruction(JsrAddr, address);
			return this as T;
		}


		/// <summary>
		/// Multiply Accumulator by 2.
		/// Bytes: 1.
		/// </summary>
		public T MultiplyBy2()
		{
			AddInstruction(Asl);
			return this as T;
		}


		/// <summary>
		/// Pop stack value into Accumulator.
		/// Bytes: 1.
		/// </summary>
		public T PullFromStack()
		{
			AddInstruction(Pla);
			return this as T;
		}


		/// <summary>
		/// Pop stack value into X register.
		/// Bytes: 1.
		/// </summary>
		public T PullFromStackIntoX()
		{
			AddInstruction(Plx);
			return this as T;
		}


		/// <summary>
		/// Pop stack value into Y register.
		/// Bytes: 1.
		/// </summary>
		public T PullFromStackIntoY()
		{
			AddInstruction(Ply);
			return this as T;
		}


		/// <summary>
		/// Pull stack into processor status.
		/// Good for restoring registers to 8 or 16 bits after a stack push.
		/// </summary>
		public T PullProcessorStatusBits()
		{
			AddInstruction(Plp);
			return this as T;
		}


		/// <summary>
		/// Push processor status flags to stack.
		/// Good to store whether registers are in 8 or 16 bit mode.
		/// </summary>
		public T PushProcessorStatusBits()
		{
			AddInstruction(Php);
			return this as T;
		}


		/// <summary>
		/// Push Accumulator value onto the stack.
		/// Bytes: 1.
		/// </summary>
		public T PushToStack()
		{
			AddInstruction(Pha);
			return this as T;
		}


		/// <summary>
		/// Push X register value onto the stack.
		/// Bytes: 1.
		/// </summary>
		public T PushXToStack()
		{
			AddInstruction(Phx);
			return this as T;
		}


		/// <summary>
		/// Push Y register value onto the stack.
		/// Bytes: 1.
		/// </summary>
		public T PushYToStack()
		{
			AddInstruction(Phy);
			return this as T;
		}


		/// <summary>
		/// Set the Accumulator to a given byte value.
		/// Bytes: 2.
		/// </summary>
		/// <param name="value">Byte to set Accumulator to.</param>
		public T Set(byte value)
		{
			AddInstruction(LdaConst, value);
			return this as T;
		}


		/// <summary>
		/// Sets the bits specified on processor status flags.
		/// Generally used to set registers 8 or 16 bits.
		/// Bytes: 2.
		/// </summary>
		/// <param name="value">Value to set </param>
		public T SetProcessorStatusBits(byte value)
		{
			AddInstruction(SepConst, value);
			return this as T;
		}


		/// <summary>
		/// Set 8-bit X register to a given byte value.
		/// Bytes: 2.
		/// </summary>
		/// <param name="value">Byte to set X register to.</param>
		public T SetX(byte value)
		{
			AddInstruction(LdxConst, value);
			return this as T;
		}


		/// <summary>
		/// Set 16-bit X register to a given byte value.
		/// Bytes: 3.
		/// </summary>
		/// <param name="value">Byte to set X register to.</param>
		public T SetX16(ushort value)
		{
			AddInstruction(LdxConst16, value);
			return this as T;
		}


		/// <summary>
		/// Set Y register to an address in direct page.
		/// Bytes: 2.
		/// </summary>
		/// <param name="address">Address in direct page to get value of.</param>
		public T SetYFromDirectPage(byte address)
		{
			AddInstruction(LdyDp, address);
			return this as T;
		}


		/// <summary>
		/// Store the Accumulator value at a given address in same bank.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to store Accumulator into.</param>
		public T StoreValueInAddress(ushort address)
		{
			AddInstruction(StaAddr, address);
			return this as T;
		}


		/// <summary>
		/// Store the Accumulator value at a given address in same bank, at index X.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to store Accumulator into.</param>
		public T StoreValueInAddressWithXIndex(ushort address)
		{
			AddInstruction(StaAddrX, address);
			return this as T;
		}


		/// <summary>
		/// Store the Accumulator value at a given address in a different bank.
		/// Bytes: 4.
		/// </summary>
		/// <param name="address">3 byte address to store Accumulator into.</param>
		public T StoreValueInBankAddress(uint address)
		{
			AddInstruction(StaLong, address);
			return this as T;
		}


		/// <summary>
		/// Store the Accumulator in the Direct Page register.
		/// Bytes: 1.
		/// </summary>
		public T StoreValueInDirectPage()
		{
			AddInstruction(Tcd);
			return this as T;
		}


		/// <summary>
		/// Store the Accumulator value in the Direct Page register at given address.
		/// Bytes: 2.
		/// </summary>
		/// <param name="address">Address in Direct Page to store value at.</param>
		public T StoreValueInDirectPage(byte address)
		{
			AddInstruction(StaDp, address);
			return this as T;
		}


		/// <summary>
		/// Store the Accumulator in the X register.
		/// Bytes: 1.
		/// </summary>
		public T StoreValueInX()
		{
			AddInstruction(Tax);
			return this as T;
		}


		/// <summary>
		/// Store the Accumulator in the Y register.
		/// Bytes: 1.
		/// </summary>
		public T StoreValueInY()
		{
			AddInstruction(Tax);
			return this as T;
		}


		/// <summary>
		/// Store the X register in the Y register.
		/// Bytes: 1.
		/// </summary>
		public T StoreXInY()
		{
			AddInstruction(Txy);
			return this as T;
		}


		/// <summary>
		/// Store the Y register in the X register.
		/// Bytes: 1.
		/// </summary>
		public T StoreYInX()
		{
			AddInstruction(Tyx);
			return this as T;
		}


		/// <summary>
		/// Subtract a numeric value from the Accumulator.
		/// Bytes: 2 (3 if 16-bit Accumulator).
		/// </summary>
		/// <param name="amount">How much to subtract.</param>
		public T Subtract(byte amount)
		{
			AddInstruction(SbcConst, amount);
			return this as T;
		}


		/// <summary>
		/// Subtract a numeric value from a given address in the same bank from the Accumulator.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to get value from.</param>
		public T SubtractAddressValue(ushort address)
		{
			AddInstruction(SbcAddr, address);
			return this as T;
		}


		/// <summary>
		/// Subtract a numeric value from a given address in same bank, at index X, from the Accumulator.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to get value from.</param>
		public T SubtractAddressValueAtIndexX(ushort address)
		{
			AddInstruction(SbcAddrX, address);
			return this as T;
		}


		/// <summary>
		/// Subtract a numeric value from a given address in same bank, at index Y, from the Accumulator.
		/// Bytes: 3.
		/// </summary>
		/// <param name="address">2 byte address to get value from.</param>
		public T SubtractAddressValueAtIndexY(ushort address)
		{
			AddInstruction(SbcAddrY, address);
			return this as T;
		}


		/// <summary>
		/// Terminate subroutine executed in different bank.
		/// Bytes: 1.
		/// </summary>
		public T TerminateBankSubroutine()
		{
			AddInstruction(Rtl);
			return this as T;
		}


		/// <summary>
		/// Terminate subroutine in same bank.
		/// Bytes: 1.
		/// </summary>
		public T TerminateSubroutine()
		{
			AddInstruction(Rts);
			return this as T;
		}


		/// <summary>
		/// Set Accumulator to use 16 bits.
		/// Bytes: 2.
		/// </summary>
		/// <returns></returns>
		public T Use16BitAccumulator()
		{
			return ClearProcessorStatusBits(0x20);
		}


		/// <summary>
		/// Set Accumulator to use 8 bits.
		/// Bytes: 2.
		/// </summary>
		/// <returns></returns>
		public T Use8BitAccumulator()
		{
			return SetProcessorStatusBits(0x20);
		}


		#region Helper Methods
		private byte GetRelativeAddress(ushort address)
		{
			uint instructionOffset = (uint)(this.currentOffset + GetBufferLength(this.currentOffset));
			sbyte relativeAddress =
				(sbyte)((address - LongToAbsolute(instructionOffset))
						- 2);

			return (byte)relativeAddress;
		}


		private ushort LongToAbsolute(uint longAddress)
		{
			byte[] currentOffsetBytes = BitConverter.GetBytes(longAddress);
			currentOffsetBytes[2] = 0x00;
			return BitConverter.ToUInt16(currentOffsetBytes, 0);
		}
		#endregion
	}


	public class Asm65816Patcher : Asm65816Patcher<Asm65816Patcher>
	{
	}
}