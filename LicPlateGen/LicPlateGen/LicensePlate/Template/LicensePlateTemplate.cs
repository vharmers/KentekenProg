using System;

namespace LicPlateGen
{
	/// <summary>
	/// 	A LicensePLateTemplate consists of multiple LicenseGroupTemplates.
	/// 	This template is used in the LicensePlate class to convert a decimal value into a licenseplate. 	 
	/// </summary>
	public struct LicensePlateTemplate
	{
		public LicenseGroupTemplate[] LicGroups {get;set;}
	}
}

