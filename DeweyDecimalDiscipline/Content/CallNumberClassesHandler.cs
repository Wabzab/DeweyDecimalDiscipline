using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DeweyDecimalDiscipline.Content
{
    public static class CallNumberClassesHandler
    {
        public static Tree GetCallNumberTree()
        {
            string PATH = "../../../Content/DeweyDecimalClasses.json";
            string CallNumberClasses = File.ReadAllText(PATH);
            Node? root = JsonConvert.DeserializeObject<Node>(CallNumberClasses);
            Tree tree = root != null ? new Tree() { Root = root } : new Tree();
            return tree;
        }
    }

    public class Tree
    {
        public Node Root { get; set; }

        public Tree() 
        {
            Root = new Node();
            ParentChildNodes(Root);
        }

        public void ParentChildNodes(Node node)
        {
            foreach (var child in node.Children)
            {
                child.Parent = node;
                ParentChildNodes(child);
            }
        }
    }

    public class Node
    {
        public Node? Parent { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public Node[] Children { get; set; }

        public Node()
        {
            Number = "";
            Description = "Default description";
            Children = new Node[0];
        }

        public int GetHeight()
        {
            int height = 0;
            Node current = this;
            while (current.Parent != null)
            {
                height++;
                current = current.Parent;
            }
            return height;
        }
    }
}
