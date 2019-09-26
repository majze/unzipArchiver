# unzipArchiver
Unzips and sorts files from subdirectories based on a top-level directory. This program is short and easily readable, so feel free to adapt to your situation.

## Program.cs
### static void Main(string[] args)
The only argument passed into the program is the top-level directory. For my use case, this was a year like "Taxes2018". the Main function builds a directory list of all folders inside the top directory. In my case, there are 12 folders like "Jan", "Feb" etc. The Main function then assigns each subdirectory to a core on the CPU and sends that subdirectory to the Extractor function. When finished, it writes to the console "** Job Complete **"

### static void Extractor(string dir)
Inside each subdirectory, there will be an unknown amount of archive files. If there are any available cores left, a subprocess will start unzipping on each archive. This example creates three new directories inside each subdirectory: a CD folder to store the original archives, a PDF folder to extract all the PDFs to, and an XML folder to extract all the XML files to.

You can change both the number of folders and the types of files that get moved there to fit your needs. This program calls a 7zip process on each archive to copy files to their respective folders instead of a blanket unzip. This way only the files that need to be extracted are processed and the entire archive can remain intact.

## backpostZippy.exe

## Dependencies
This program assumes you have [7-zip installed](https://www.7-zip.org/download.html), as it calls a subprocess on the executable. If your 7zip is installed in a non-native location, or you prefer to use WinRAR or some other unpacking tool, change the code accordingly where the variable 'zipExePath' is.

I have never run this on a machine that did not have some sort of Visual Studio, Microsoft, or C++ redistributable installed. If you have any issues running this on a Windows machine, let me know.

## Special Thanks
I'd like to personally thank Zoidberg for assisting with the conversion from a Python prototype, as well as introducing multi-threading to the program.
