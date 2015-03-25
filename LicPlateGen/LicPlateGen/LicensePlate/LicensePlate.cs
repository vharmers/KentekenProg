using System;
using System.Threading;
using System.Text;

namespace LicPlateGen
{
	/// <summary>
	/// The LicensePlate class is used to generate Licenseplates. 
	/// The LicensePLate class uses a LicenseTemplate object as "blueprint"  
	/// </summary>

	public class LicensePlate
	{

		/// <summary>
		/// 	Possible characters for licenseplate generation
		/// </summary>
		private static readonly char[] valitChars = new char[] {
			'A', 'B', 'D', 'E', 'F', 
			'G', 'H', 'I', 'J', 'K', 'N', 'M', 
			'L', 'O', 'P', 'R', 'S' , 'T', 
			'U', 'V', 'W', 'X', 'Y', 'Z'};
		

		/// <summary>
		/// The template object which is used as blueprint.
		/// </summary>
		private readonly LicensePlateTemplate template;

		/// <summary>
		/// A ThreadLocal StringBuilder. Each thread gets his own stringbuilder instance.
		/// </summary>
		private ThreadLocal<StringBuilder> localBuilder = new ThreadLocal<StringBuilder>(() => new StringBuilder());

		/// <summary>
		/// Initializes a new instance of the <see cref="LicPlateGen.LicensePlate"/> class.
		/// </summary>
		/// <param name="template">
		/// 	The Template which this class uses blueprint. 
		/// </param>
		public LicensePlate (LicensePlateTemplate template)
		{
			this.template = template;
		}

		/// <summary>
		/// Converts a decimal value to a licenseplate.
		/// </summary>
		/// <returns>
		/// 	The generated licenseplate
		/// </returns>
		/// <param name="decimalValue">
		/// 	The decimal value
		/// </param>
		public string ConvertDecimalToLicensePlate(int decimalValue)
		{
			int modulo;

			//This for-loop iterates over the diffent licenseplate groups. The innerloop of this
			//for-loop interates over each character/number and generates a value using the algoritm
			for(int groupCounter = 0; groupCounter < template.LicGroups.Length; groupCounter++)
			{
				LicenseGroupTemplate currentGroup = template.LicGroups[groupCounter];

				for(int groupCharCounter = 0; groupCharCounter < currentGroup.Count; groupCharCounter++)
				{
					//	This algoritm calculates a character or number by using the DivRem function
					if (currentGroup.GroupType.Equals (LicType.Number)) 
					{
						//Calculate a number by getting the modulo. And setting the defided value as the new decimal value
						decimalValue = Math.DivRem (decimalValue, 10, out modulo);
						localBuilder.Value.Insert (0, modulo);
					} 
					else //GroupType is Character
					{
						decimalValue = Math.DivRem (decimalValue, valitChars.Length, out modulo);
						localBuilder.Value.Insert (0, valitChars[modulo]);
					}

				}

				//Add a "-" when needed
				if (groupCounter < template.LicGroups.Length - 1) 
				{
					localBuilder.Value.Insert (0, '-');
				}

			}

			string generatedLicense = localBuilder.Value.ToString ();
			//Clear the StringBuilder so it can be reused in the following iteration
			localBuilder.Value.Clear ();
			return generatedLicense;
		}
	}
}

