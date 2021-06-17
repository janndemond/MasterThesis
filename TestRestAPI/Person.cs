using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LINQtoCSV;
namespace TestRestAPI
{
    class  ListPersons
    {
        private List<Person> _users;

        public List<Person> Users => _users;

        private string _path;
        public ListPersons(string path)
        {
            _users = new List<Person>();
            _path = path;
            if (File.Exists(path))
            {
                LoadListFromFile();
            }
            
        }

        public bool listToCSV()
        {
            CsvFileDescription outputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ';', // tab delimited
                FirstLineHasColumnNames = true, // no column names in first record
               
            };
            CsvContext cc = new CsvContext();
            cc.Write(
                _users,
                _path,
                outputFileDescription);
            return true;
        }

        private bool LoadListFromFile()
        {
            var temp =File.ReadAllLines(_path);
            if (temp.Length > 0)
            {
                int i = 0;
                foreach (var line in temp)
                {
                    if (i == 0)
                    {
                        i++;
                        continue;
                    }
                    var tempSplit = line.Split(";");
                    
                    if (tempSplit.Length == 3)
                    {
                        if (string.IsNullOrEmpty(tempSplit[0]) || string.IsNullOrEmpty(tempSplit[1]) ||
                            string.IsNullOrEmpty(tempSplit[2]))
                        {
                            continue;
                        }

                        i++;
                        
                        var tempPerson = new Person(tempSplit[0]);
                        tempPerson.RandomNum = tempSplit[1];
                        tempPerson.Hash = tempSplit[2];
                        _users.Add(tempPerson);
                        
                    }
                    else
                    {
                        Console.WriteLine("Prom beim einlesen der Zeile: " + line);
                    }
                }
                Console.WriteLine(i.ToString()+" Persons have been loaded from: "  + _path);
                return true;
            }
            Console.WriteLine("Prom beim einlesen der der Datei: " + _path);
            return false;

        }

        public string GetAnonymPerson(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "";    
            }

            if (_users.Exists( x => x.Name == name))
            {
                return _users.Single(x => x.Name == name).Hash;
            }
           
            var temp = new Person(name);
            _users.Add(temp);
            return temp.Hash;
        }
    }
    class Person
    {
        private readonly string name;
        private string randomNum;
        [CsvColumn(Name = "Name", FieldIndex = 1)]
        public string Name => name;
        [CsvColumn(Name = "RandomNum", FieldIndex = 2)]
        public string RandomNum
        {
            get => randomNum;
            set => randomNum = value;
        }
        [CsvColumn(Name = "Hash", FieldIndex = 3)]
        public string Hash
        {
            get => hash;
            set => hash = value;
        }

        private string hash;
        

        public Person(string name)
        {
            this.name = name;
            Random rd = new Random();
            randomNum = rd.Next(0, 10000).ToString();
            hash = CreateMD5(name + randomNum);
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}