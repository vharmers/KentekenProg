using System;

namespace LicPlateGen
{
	public struct LicenseChar
	{
		public static readonly LicType[,] series = new LicType[,] 
		{ 
			//Serie 1 - XX-99-99	
			{ LicType.Character, LicType.Character, LicType.Number, LicType.Number, LicType.Number, LicType.Number },
			//Serie 2 - 99-99-xx
			{ LicType.Number, LicType.Number, LicType.Number, LicType.Number, LicType.Character, LicType.Character },
			//Serie 3 - 99-xx-99
			{ LicType.Number, LicType.Number, LicType.Character, LicType.Character, LicType.Number, LicType.Number },
			//Serie 4 - xx-99-xx
			{ LicType.Character, LicType.Character, LicType.Number, LicType.Number, LicType.Character, LicType.Character },
			//Serie 5 - xx-xx-99
			{ LicType.Character, LicType.Character, LicType.Character, LicType.Character, LicType.Number, LicType.Number},
			//Serie 6 - 99-xx-xx
			{  LicType.Number, LicType.Number, LicType.Character, LicType.Character, LicType.Character, LicType.Character},
			//Serie 7 - 99-xxx-9
			{ LicType.Number, LicType.Number, LicType.Character, LicType.Character, LicType.Character, LicType.Number},
			//Serie 8 - 9-xxx-99
			{ LicType.Number, LicType.Character, LicType.Character, LicType.Character, LicType.Number, LicType.Number},
			//Serie 9 - xx-999-x
			{ LicType.Character, LicType.Character, LicType.Number, LicType.Number, LicType.Number, LicType.Character},
			//Serie 10 - x-999-xx
			{ LicType.Character, LicType.Number, LicType.Number, LicType.Number, LicType.Character, LicType.Character},
			//Serie 11 - xxx-99-x
			{ LicType.Character, LicType.Character, LicType.Character, LicType.Number, LicType.Number, LicType.Character}
		};
		
		public LicType Type;
		public int Value;
	}
}

