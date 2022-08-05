namespace SnesEditing
{
	public struct Op
	{
		/// <summary>
		/// Opcode in Assembly.
		/// </summary>
		public readonly string Assembly;

		/// <summary>
		/// Opcode in hexadecimal.
		/// </summary>
		public readonly byte Hex;

		/// <summary>
		/// Number of bytes instruction uses.
		/// </summary>
		public readonly int Length;


		public Op(string assembly, byte hex, int length)
		{
			this.Assembly = assembly;
			this.Hex = hex;
			this.Length = length;
		}
	}
}
