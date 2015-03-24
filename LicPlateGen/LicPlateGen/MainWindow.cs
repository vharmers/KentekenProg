using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Gtk;
using LicPlateGen;


/// <summary>
/// 	This is the MainWindow of the Application. All the application logic lives here
/// </summary>
public partial class MainWindow: Gtk.Window
{	
	/// <summary>
	/// Initializes a new instance of the <see cref="MainWindow"/> class.
	/// </summary>
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		ProgressLabel.Text = "Gereed";
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

	/// <summary>
	/// Generates licenseplates and saves the result to a file. This method is threadsave
	/// </summary>
	/// <param name="outputFilePath">Output file path.</param>
	/// <param name="generator">Generator which generates the licenseplates</param>
	/// <param name="plateCount">The amount of licenseplate you want to generate</param>
	/// <param name="checkForDuplicates">If set to <c>true</c> check for dupilcate licenseplates.</param>
	private void GenerateAndSavePLates(string outputFilePath, LicensePlate generator, int plateCount, bool checkForDuplicates = false)
	{
		string[] result = GenerateLicensePlates (generator, plateCount);

		Task fileTask = new Task(() => System.IO.File.WriteAllLines (outputFilePath, result));
		fileTask.Start ();

		if (checkForDuplicates) 
		{
			string[] doubleResults = CheckForDoubleResults (result);

			if (doubleResults.Count () > 0) 
			{
				Gtk.Application.Invoke(delegate 
				{
					WarnForDoubleResults (doubleResults);
				});

				
			} 
			else 
			{
				Gtk.Application.Invoke(delegate 
				{
					InformNoDoubleResults ();
				});
					
			}
		}
		fileTask.Wait ();
	}

	/// <summary>
	/// 	Checks a string array for duplicate entrys
	/// </summary>
	/// <returns>A string array with duplicate entrys or an empty array if there a not duplicate entrys</returns>
	/// <param name="results">Results.</param>
	private string[] CheckForDoubleResults(string[] results)
	{
		var duplicates = results
			.GroupBy (x => x)
			.AsParallel()
			.Where (g => g.Count () > 1)
			.Select (y => y.Key)
			.ToArray ();
		return duplicates;
	}

	private void WarnForDoubleResults(string[] doubleResults)
	{
		string dialogMessage = "Operation complete but the folowing duplicates where found:\n";
		foreach (string dResult in doubleResults) 
		{
			dialogMessage += dResult + "\n";
		}
		MessageDialog warnDialog = new MessageDialog (this, DialogFlags.Modal, MessageType.Warning, ButtonsType.Ok, dialogMessage);
		warnDialog.Run ();
		warnDialog.Destroy ();
	}

	private void InformNoDoubleResults()
	{
		MessageDialog infoDialog = new MessageDialog (this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "No duplicates found");
		infoDialog.Run ();
		infoDialog.Destroy ();
	}

	/// <summary>
	/// Generates the license plates. The licenseplates are generates in a Parallel for loop.
	/// </summary>
	/// <returns>The license plates.</returns>
	/// <param name="generator">Generator.</param>
	/// <param name="plateCount">Plate count.</param>
	private string[] GenerateLicensePlates(LicensePlate generator, int plateCount)
	{
		string[] result = new string[plateCount];

		Parallel.For (0, plateCount, (counter) => {
			string plate = generator.ConvertDecimalToLicensePlate (counter);
			result [counter] = plate;
		});

		return result;
	}

	/// <summary>
	/// Triggered when the user clicks on the generate button
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	protected void OnGenButtonClicked (object sender, EventArgs e)
	{
		//Collect information
		int serie = SerieComboBox.Active + 5;
		int plateCount = (int)CountSpinbutton.Value;
		string filePath = FileChooser.Filename;
		bool checkDuplicates = DuplicateCheckbox.Active;

		//Initialize the needed components
		LicensePlateTemplate template = LicTempFactory.CreateSetTemplate (serie);
		LicensePlate plate = new LicensePlate (template);

		//Execute the work
		ProgressLabel.Text = "Genereren";
		
		Thread myThread = new Thread (() => {
			GenerateAndSavePLates(filePath, plate, plateCount, checkDuplicates);
			Gtk.Application.Invoke(delegate 
			{
				ProgressLabel.Text = "Klaar";
			});
		}); 
		myThread.Start ();

	}
}
