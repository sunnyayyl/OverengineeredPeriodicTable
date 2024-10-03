// Tad bit overengineered
namespace OverengineeredPeriodicTable
{
    
    public enum Group
    {
        AlkaliMetal,
        AlkalineEarthMetal,
        NobleGases
    }
    public interface IEntry
    {
        public string Name { get; }
        public string Symbol { get; }
        public double Mass { get; }
        public double Proton { get; }
        public Group Group { get; }
    }
    public class AlkaliMetal(string name,string symbol,double mass, double proton):IEntry
    {
        public string Name { get; private set; } = name;
        public string Symbol { get; private set; } = symbol;
        public double Mass { get; private set; }= mass;
        public double Proton { get; private set; }= proton;
        public Group Group { get; private set; } = Group.AlkaliMetal;
    }
    public class AlkalineEarthMetal(string name,string symbol,double mass, double proton):IEntry
    {
        public string Name { get; private set; } = name;
        public string Symbol { get; private set; } = symbol;
        public double Mass { get; private set; }= mass;
        public double Proton { get; private set; }= proton;
        public Group Group { get; private set; } = Group.AlkalineEarthMetal;
    }
    public class NobelGases(string name,string symbol,double mass, double proton):IEntry
    {
        public string Name { get; private set; } = name;
        public string Symbol { get; private set; } = symbol;
        public double Mass { get; private set; }= mass;
        public double Proton { get; private set; }= proton;
        public Group Group { get; private set; } = Group.NobleGases;
    }
    public class LookUpDictionary(Dictionary<string, IEntry> nameLookup,Dictionary<string, IEntry> symbolLookup,Dictionary<Group, IEntry[]> groupLookup)
    {
        public Dictionary<string, IEntry> NameLookup { get; private set; } = nameLookup;
        public Dictionary<string, IEntry> SymbolLookup { get; private set; } = symbolLookup;
        public Dictionary<Group, IEntry[]> GroupLookup { get; private set; } = groupLookup;
    }
    public static class Constants
    {
        private static LookUpDictionary GenerateDictionary(IEntry[] entries)
        {
            var nameLookup= new Dictionary<string, IEntry>();
            var symbolLookup = new Dictionary<string, IEntry>();
            var groupLookup = new Dictionary<Group, IEntry[]>();
            foreach (var entry in entries)
            {
                symbolLookup[entry.Symbol.ToLower()] = entry;
                nameLookup[entry.Name.ToLower()] = entry;
                if (groupLookup.ContainsKey(entry.Group))
                {
                    groupLookup[entry.Group]=groupLookup[entry.Group].Append(entry).ToArray();
                }
                else
                {
                    groupLookup.Add(entry.Group, new IEntry[] { entry });
                }
            }
            return new LookUpDictionary(nameLookup,symbolLookup,groupLookup);
        }
        public static readonly AlkaliMetal Lithium = new AlkaliMetal("Lithium", "Li", 6.94, 3);
        public static readonly AlkaliMetal Sodium = new AlkaliMetal("Sodium", "Na", 22.99, 11);
        public static readonly AlkaliMetal Potassium = new AlkaliMetal("Potassium", "K", 39.10, 19);
        public static readonly NobelGases Helium = new NobelGases("Helium", "He", 4.00, 2);
        public static readonly NobelGases Neon = new NobelGases("Neon", "Ne", 20.18, 10);
        public static readonly NobelGases Argon=new NobelGases("Argon", "Ar", 39.95, 18);
        public static readonly AlkalineEarthMetal Beryllium = new AlkalineEarthMetal("Beryllium", "Be", 9.01, 4);
        public static readonly AlkalineEarthMetal Magnesium = new AlkalineEarthMetal("Magnesium", "Mg", 24.31, 12);
        public static readonly AlkalineEarthMetal Calcium = new AlkalineEarthMetal("Calcium", "Ca", 40.08, 20);
        public static readonly LookUpDictionary LookUp =
            Constants.GenerateDictionary([Constants.Lithium, Constants.Sodium, Constants.Potassium, Constants.Helium, Constants.Neon, Constants.Argon,Constants.Beryllium,Constants.Magnesium,Constants.Calcium]);
       

        public static readonly Dictionary<Group, string> GroupToStringLookup = new Dictionary<Group, string>
        {
            { Group.AlkaliMetal, "Alkali metals" },
            { Group.NobleGases , "Noble gases" },
            { Group.AlkalineEarthMetal , "Alkaline earth metals" },
        };

        public static readonly Dictionary<string, Group> StringToGroupLookup = GroupToStringLookup.ToDictionary(x=>x.Value.ToLower(),x=>x.Key);
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(new string('#',39));
                Console.WriteLine("Please enter a symbol, name, or group.");
                var input = Console.ReadLine()!.ToLower();
                IEntry[] elements;
                
                if (Constants.StringToGroupLookup.TryGetValue(input,out var group))
                {
                    elements = Constants.LookUp.GroupLookup[group];
                }else if (Constants.LookUp.NameLookup.TryGetValue(input, out var element1))
                {
                    elements=[element1];
                }else if (Constants.LookUp.SymbolLookup.TryGetValue(input, out var element2))
                {
                    elements=[element2];
                }
                else
                {
                    Console.WriteLine("Please enter a valid symbol, name, or group.");
                    Console.WriteLine();
                    continue;
                }

                foreach (var entry in elements)
                {
                    Console.WriteLine($"Element: {entry.Name} ({entry.Symbol})");
                    Console.WriteLine($"Weight: {entry.Mass}");
                    Console.WriteLine($"Group: {Constants.GroupToStringLookup[entry.Group]}");
                }
                Console.WriteLine();
            }
            
            
        }
    }
}
