﻿using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class Group
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }

        public Group() { }
        public Group(string groupName, string groupHeader, string groupFooter)
        {
            Name = groupName;
            Header = groupHeader;
            Footer = groupFooter;
        }

        public static Group GetRandom()
        {
            var faker = new Faker();
            return new Group(faker.Random.Word(), faker.Random.Words(5), faker.Random.Words(10));
        }
    }
}
