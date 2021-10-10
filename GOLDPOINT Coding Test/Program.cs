using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLDPOINT_Coding_Test
{
    class Program
    {
        //Q: What is the name of the bottom tag that is given in your input file?
        //A: It's called a Root
        static void Main(string[] args)
        {
            //////////beginning of program//////////
            List<Tag> tags=new List<Tag>();
            string[]input=ReadData();
            ParseData(input, tags);

            for(int i=0;i<tags.Count-1;i++){
                var r=Traverse(tags[i], tags[i+1],tags);
                if(r!=null){
                tags[i]=r;
                }
                tags[i].Print("");
                Console.WriteLine("");
            }
            ////////////////////////////////////////
            string[] ReadData()
            {
                string[] input = System.IO.File.ReadAllLines(@"interview_test_input.txt");
                return input;
            }

            void ParseData(string[] x, List<Tag> list)
            {
                foreach(string line in x)
                {
                    string[] parseStrings = { "->", "," };
                    string[] data=line.Split(parseStrings, System.StringSplitOptions.RemoveEmptyEntries);

                    if(data.Length>1){
                        Tag newTag=CreateTag(data);
                        list.Add(newTag);
                    }
                    
                }
            }
            Tag CreateTag(string[] data){
                string tagName=data[0].Trim();
                Tag root = new Tag(tagName);
                    for(int i=1;i<data.Length;i++){
                        string childName=data[i].Trim();
                        Tag child=new Tag(childName);
                        root.Add(child);
                    }
                return root;
            }

             Tag Traverse(Tag tag, Tag s,List<Tag> tags){
                if(tag!=null){
                    //found it
                    if(tag.Name==s.Name){
                        foreach(Tag child in tag.Children){
                            s.Add(child);
                        }
                        //Console.WriteLine($"Child match: {tag.Name}");
                        return tag;
                        
                    }
                    foreach(Tag child in tag.Children){
                        Traverse(s, child, tags);
                    }
                }
                return null;
            }                        
        }

    }
    class Tag
    {
        private string name;
        private List<Tag> children;
        public Tag(string n)
        {
            name=n;
            children=new List<Tag>();
        }
        public string Name{
            get{return name;}
            set{name=value;}
        }
        public List<Tag> Children{
            get{return children;}
        }
        public void Print(string prefix)
        {
        Console.WriteLine($"{prefix} + {this.Name} ");
        foreach (Tag n in Children){
            if (Children.IndexOf(n) == Children.Count - 1){
                 n.Print(prefix + "    ");
            }else{
                n.Print(prefix + "   |");
            } 
        }
        }
        public void Add(Tag child){
            children.Add(child);
        }

    }

}
