using System;
using System.Threading;
using System.Text;

namespace LicPlateGen
{
	public class LicensePlate
	{
		private static readonly char[] valitChars = new char[] {
			'A', 'B', 'D', 'E', 'F', 
			'G', 'H', 'I', 'J', 'K', 'N', 'M', 
			'L', 'O', 'P', 'R', 'S' , 'T', 
			'U', 'V', 'W', 'X', 'Y', 'Z'};
		
		
		private readonly LicensePlateTemplate template;
		private ThreadLocal<StringBuilder> localBuilder = new ThreadLocal<StringBuilder>(() => new StringBuilder());

		public LicensePlate (LicensePlateTemplate template)
		{
			this.template = template;
		}
		
		public string ConvertDecimalToLicensePlate(int decimalValue)
		{
			int modulo;
			
			for(int groupCounter = 0; groupCounter < template.LicGroups.Length; groupCounter++)
			{
				LicenseGroupTemplate currentGroup = template.LicGroups[groupCounter];

				for(int groupCharCounter = 0; groupCharCounter < currentGroup.Count; groupCharCounter++)
				{
					if (currentGroup.GroupType.Equals (LicType.Number)) 
					{
						decimalValue = Math.DivRem (decimalValue, 10, out modulo);
						localBuilder.Value.Insert (0, modulo);
					} 
					else //GroupType is Character
					{
						decimalValue = Math.DivRem (decimalValue, valitChars.Length, out modulo);
						localBuilder.Value.Insert (0, valitChars[modulo]);
					}

				}

				if (groupCounter < template.LicGroups.Length - 1) 
				{
					localBuilder.Value.Insert (0, '-');
				}

			}

			string generatedLicense = localBuilder.Value.ToString ();
			localBuilder.Value.Clear ();
			return generatedLicense;
		}
	}
}

