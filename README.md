# DotA2StatsParser
A C# library for parsing data from [Dotabuff](http://www.dotabuff.com/) and [Yasp](https://yasp.co/).

## Description
This library was designed for real time analysis of data from [Dotabuff](http://www.dotabuff.com/) and [Yasp](https://yasp.co/).
It will load the HTML representation of the requested data and parse it in an easy to use way.

Parsing the HTML is done with the help of the [Html Agility Pack](https://htmlagilitypack.codeplex.com/).

This assembly includes mapping files written with JSON. 
To parse the JSON files this assembly uses Newtonsoft's [JSON.Net Framework](http://www.newtonsoft.com/json).

## Sidenote

This current version (0.1.0.0) is in an early stage of development.

There are a still a lot of things that have to be done:

- Internal code documentation and summaries are missing
- Thorough exception handling
- Better XPaths for selecting data from HTML and using it in code respectively
- General refactoring of code

If you find bugs while using this assembly or only by looking at the code please feel free to open an issue. I will try to fix it as fast as possible.
Everyone is welcome to create a fork and to implement away. 

## Requirements

- .NET Framework 4.5
- [Html Agility Pack](https://htmlagilitypack.codeplex.com/)
- [JSON.Net Framework](http://www.newtonsoft.com/json)
- Internet Connection

## Installation
Via NuGet:
```
Install-Package DotA2StatsParser 
```

Manual installation:

1. Get the [latest binaries](https://github.com/Shamshiel/DotA2StatsParser/releases/latest)  
2. Get the [JSON Framework .dll by Newtonsoft](https://github.com/JamesNK/Newtonsoft.Json/releases)  (Version 8.0.2 or higher)
3. Extract Newtonsoft.Json.dll from `Bin\Net45\Newtonsoft.Json.dll`  
4. Get the [Html Agility Pack](https://htmlagilitypack.codeplex.com/)  (1.4.6 or higher)
5. Extract HtmlAgilityPack.dll from `Net45\HtmlAgilityPack.dll`
6. Add a reference to DotA2StatsParser.dll, HtmlAgilityPack.dll and Newtonsoft.Json.dll in your project  

## How to use

Create a `Dataparser` instance (Internet connection required) and call one of the available methods of the newly created instance to get the desired data.
The `Dataparser` implements IDisposable so it is possible to use it in a using statement.

It is possible and advised to perform a health check before calling any methods. If the health check fails the other methods most likely will too!

## Example

This short example uses all available functions (Version 0.1.0.0)

```C#
using (DotA2StatsParser.Dataparser dataparser = new DotA2StatsParser.Dataparser())
{
	IHealthCheckResult result = dataparser.PerformHealthCheck();

	if (result.IsDotabuffAvailable)
	{
		IHero trollWarlord = dataparser.GetHeroPageData(Heroes.TrollWarlord);
		IItem helmOfTheDominator = dataparser.GetItemPageData(Items.HelmOfTheDominator);
		IPlayer player1 = dataparser.GetPlayerPageData("106863163");

		IEnumerable<IMatchExtended> matchList = dataparser.GetPlayerMatchesPageData(player1.Id, new PlayerMatchesOptions() { Duration = Durations.Between20And30 });
	}

	if (result.IsYaspAvailable)
	{
		IEnumerable<IWordCloud> playerWordCount = dataparser.GetWordCloud("106863163");
	}
}
```