using System;
using System.Text.RegularExpressions;
using LogXtreme.Extensions;

namespace RegularExpressionTester {

    /// <summary>
    /// This simple program is used to test regular expressions.
    /// 
    /// ----------------------
    /// Example 1:
    /// 
    /// pattern=
    /// (cat)+ 
    /// subject=
    /// catdog
    /// True @0:3
    ///     g0 = cat
    ///     g1 = cat
    ///     
    /// pattern=
    /// cat|dog
    /// subject=
    /// cat
    /// True @0:3
    ///     g0 = cat
    ///     
    /// pattern=
    /// cat|dog
    /// subject=
    /// dog
    /// True @0:3
    ///     g0 = dog
    ///     
    /// pattern=
    /// cat|dog
    /// subject=
    /// dogcat
    /// True @0:3
    ///     g0 = dog
    /// True @3:3
    ///     g0 = cat
    /// 
    /// pattern=
    /// (\w+)\s+meet\s+(\w+)
    /// subject=
    /// Sam meet Jan
    /// True @0:12
    ///     g0 = Sam meet Jan
    ///     g1 = Sam
    ///     g2 = Jan
    /// 
    /// e:exit
    /// ----------------------
    /// 
    /// </summary>
    class Program {

        static void Main(string[] args) {

            bool runagain = true;

            do {               

                string pattern = string.Empty;
                string subject = string.Empty;

                if(args.Length > 0) {
                    pattern = args[0].Replace("\\n", "\n");
                }

                if(args.Length > 1) {
                    subject = args[1].Replace("\\n", "\n");
                }

                if(pattern.IsBlank()) {
                    Console.WriteLine("pattern=");
                    pattern = Console.ReadLine().Replace("\\n", "\n");
                    Console.WriteLine("subject=");
                    subject = Console.ReadLine().Replace("\\n", "\n");
                }

                var regex = new Regex(pattern);
                var match = regex.Match(subject);
                 

                while(match.Success) {

                    Console.WriteLine($"{match.Success} @{match.Index}:{match.Length}");

                    // For each match in the subject there may be also captured groups if the RE specified any.
                    // This goes through the groups that are found in the subject for the present match.
                    int gmatchCounter = -1;
                    foreach(var g in match.Groups) {
                        Console.WriteLine($"    g{++gmatchCounter} = {g}");
                    }

                    match = match.NextMatch();
                }

                Console.WriteLine("\n\re: exit");
                runagain = !Console.ReadLine().ToLower().Trim().StartsWith("e");

            } while(runagain);
        }
    }
}

// Groups in REs
// In Regular Expressions the term "group" is overloaded as it can 
// mean either of the following. 

// 1- a part of the regular expression pattern.
// 2- a part of the subject that matches the regular expression.

// Some facts about groups in REPs [RE Patterns]

// 1-Every REP has at least one group - the RE itself.
// 2-You can add groups to a pattern using complementary pairs of ().
// 3-Groups can be capturing or not capturing.
// 4-When a group is capturing it 

// Some examples of REPs with groups 

// fish(cat)(dog)bird has 3 groups in it
//   1-fish(cat)(dog)bird is the whole pattern and also a group
//   2-(cat) is a group 
//   3-(dog) is a group

// fish(cat(dog))bird has 3 groups in it
//   1-fish(cat(dog))bird is the whole pattern and also a group
//   2-(cat(dog)) = (catdog) is a group - no nesting!
//   3-(dog) is a group

// Groups can also encompass alternations
// fish(cat|dog)bird
//  1-fishcatbird is the whole pattern and also a group
//  2-fishdogbird is the alternate whole pattern and also a group
//  3-(cat) is a group
//  4-(dog) is a goup

//  Groups with repetitions
//  fish(cat)*bird   => look for cat repeated any number of times 
//  fish(cat){3}bird => look for cat repeated exatly 3 times
//  fish(cat){1,3}bird => look for cat repeated exatly any times between 1 & 3 times
//  fish(cat){0,1}bird => look for cat repeated exatly 1 time
//  fish(cat)?bird => look for cat repeated exatly 1 time
//  that is fish(cat){0,1}bird = fish(cat)?bird

//  The problem with the Keinie start * and the @0:0 match
//  In REs the * is called the Kleinie Star.
//  A RE with a * ALWAYS mathces as @0:0 that is in pos 0 with length of 0 even if the patterns is not in the subject.
//  In order for a pattern that uses * to not match @0:0 you need to repeat the patterns in the RE.
//  (cat)* will match @0:0 of any string subject and any sting XXXcat..cat..catXXX where cat repeats 1 or more times at the first occurrence of cat.
//  (cat)(cat)* is the same as (cat)* but DOES NOT MATCH @0:0 of any string that does not have "cat" in it!
//  (cat)(cat)* = (cat)+ => the + is a shot hand version of (pattern)(pattern)*
//  Finally (pattern)* ALWAYS matches at least as @0:0
//  While (pattern)+ only matches of the subject has "pattern" at least 1 time in it.

// The are two ways to identify a group in a RE

// 1-Ordinal Identity of grups in REs

// REs with groups are numbered relatively to other groups in the same pattern.
// There is alwys a group 0 that is the whole RE.
// Other groups are numbered 1,2,3,etc.. according to the position of their left parenthesis

// Example 1
// fish(cat)(dog)bird => fishcatdogbird is group 0
// fish(cat)(dog)bird => cat is gorup 1
// fish(cat)(dog)bird => dog is group 2

// Example 2
// REs do not nest gorups
// fish(cat(dog))bird => fishcatdogbird is group 0
// fish(cat(dog))bird => catdog is group 1
// fish(cat(dog))bird => dog is group 2

// 2-Gruops in REs can also be identified by Name
// ...