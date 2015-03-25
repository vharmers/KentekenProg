using System;

namespace LicPlateGen
{
	/// <summary>
	/// 	The LicenseGroupTemplate represents a group of characters or numbers on a licenseplate
	/// 	A licenseplateTemplate consists of multiple licenseGroupTemplates.
	/// </summary>

	public struct LicenseGroupTemplate
	{
		public LicType GroupType {get;set;}
		public Int16 Count {get;set;}
	}
}

