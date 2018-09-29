<Query Kind="Program" />

// C# Program
// ITs like you are in the body of the classic static class Program of a console app

void Main() {	

	var dpath = "C:\\Users\\davidespano\\Downloads";
	
	// a dictionary with key the file name and value the length in bytes.
	// a very functinal way...
	Directory.GetFiles(dpath)
	.Select(fpath => new FileInfo(fpath))
	.ToDictionary(fp => fp.Name, fp => fp.Length)
	.Dump();
}