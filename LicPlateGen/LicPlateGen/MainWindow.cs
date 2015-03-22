using System;
using System.Threading;
using Gtk;
using LicPlateGen;

public partial class MainWindow: Gtk.Window
{	
	
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnCloseButtonClicked (object sender, System.EventArgs e)
	{
		Application.Quit();
	}


	private void GenerateAndSavePLates(string outputFilePath, LicensePlate generator, int plateCount)
	{
		string[] result = GenerateLicensePlates (generator, plateCount);
		System.IO.File.WriteAllLines (outputFilePath, result);
	}

	private string[] GenerateLicensePlates(LicensePlate generator, int plateCount)
	{
		string[] result = new string[plateCount];

		for (int counter = 0; counter < plateCount; counter++) 
		{
			string plate = generator.ConvertDecimalToLicensePlate (counter);
			result [counter] = plate;
		}

		return result;
	}

	protected void OnGenButtonClicked (object sender, EventArgs e)
	{
		int serie = SerieComboBox.Active + 1;
		int plateCount = (int)CountSpinbutton.Value;
		string filePath = FileChooser.Filename;
		LicensePlateTemplate template = LicTempFactory.CreateSetTemplate (serie);
		LicensePlate plate = new LicensePlate (template);

		new Thread (() => GenerateAndSavePLates(filePath, plate, plateCount)).Start ();
	}
}
