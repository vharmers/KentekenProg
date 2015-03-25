using System;

namespace LicPlateGen
{
	public static class LicTempFactory
	{
		public static LicensePlateTemplate CreateSetTemplate(int setNumber)
		{
			LicensePlateTemplate returnPlate;

			switch(setNumber)
			{
				//Serie 4 - xx-99-xx
			case 4:

				returnPlate = new LicensePlateTemplate () { 
					LicGroups = new LicenseGroupTemplate[] {
						new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 2 },
						new LicenseGroupTemplate (){ GroupType = LicType.Number, Count = 2 },
						new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 2 }
					}
				};
				
				break;
				//Serie 5 - xx-xx-99
			case 5:
				
				returnPlate = new LicensePlateTemplate () { 
					LicGroups = new LicenseGroupTemplate[] {
						new LicenseGroupTemplate (){ GroupType = LicType.Number, Count = 2 },
						new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 2 },
						new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 2 }
					}
				};
				
				break;
				//Serie 6 - 99-xx-xx
				case 6:
				
					returnPlate = new LicensePlateTemplate () { 
						LicGroups = new LicenseGroupTemplate[]{
						 	new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 2 },
						 	new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 2 },
						 	new LicenseGroupTemplate (){ GroupType = LicType.Number, Count = 2 }
								}
				};
				
				break;
				//Serie 7 - 99-xxx-9
				case 7:
				
					returnPlate = new LicensePlateTemplate () { 
						LicGroups = new LicenseGroupTemplate[]{
						 	new LicenseGroupTemplate (){ GroupType = LicType.Number, Count = 1 },
						 	new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 3 },
						 	new LicenseGroupTemplate (){ GroupType = LicType.Number, Count = 2 }
								}
				};
				
				break;
				//Serie 8 - 9-xxx-99
				case 8:
				
					returnPlate = new LicensePlateTemplate () { 
						LicGroups = new LicenseGroupTemplate[]{
							new LicenseGroupTemplate (){ GroupType = LicType.Number, Count = 2 },
							new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 3 },
							new LicenseGroupTemplate (){ GroupType = LicType.Number, Count = 1 }
								}
				};
				
				break;
				//Serie 9 - xx-999-x
				case 9:
				
					returnPlate = new LicensePlateTemplate () { 
						LicGroups = new LicenseGroupTemplate[]{
							new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 1 },
							new LicenseGroupTemplate (){ GroupType = LicType.Number, Count = 3 },
							new LicenseGroupTemplate (){ GroupType = LicType.Character, Count = 2 }
								}
				};
				
				break;
				//Serie 10 - x-999-xx
				//Serie 11 - xxx-99-x
				default:
					throw new InvalidOperationException ("Set number " + setNumber + " is not supported by this factory");
				
			}
			return returnPlate;
		}

	}
}

