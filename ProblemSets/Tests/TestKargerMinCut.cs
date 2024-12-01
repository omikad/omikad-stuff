using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;

namespace Tests
{
	[TestClass]
	public class TestKargerMinCut
	{
		[TestMethod]
		public void CanFindMinCut_Graph1()
		{
			var input = new[]
			{							   	
				new [] {1, 2, 3, 4			   		},
				new [] {2, 1, 3, 4			   		},
				new [] {3, 1, 2, 4			   		},
				new [] {4, 1, 2, 3, 5		   		},
				new [] {5, 4, 6, 7, 8		   		},
				new [] {6, 5, 7, 8			   		},
				new [] {7, 5, 6, 8			   		},
				new [] {8, 5, 6, 7			   		},

			}
			.Select(a => a.Select(i => i - 1).Skip(1).ToArray()).ToArray();

			var minCut = new KargerMinCut().CalcMinCut(input);

			Assert.AreEqual(1, minCut);
		}

		[TestMethod]
		public void CanFindMinCut_Graph2()
		{
			var input = new[]
			{							   	
				new [] {1, 2, 3, 4, 7				},
				new [] {2, 1, 3, 4					},
				new [] {3, 1, 2, 4					},
				new [] {4, 1, 2, 3, 5				},
				new [] {5, 4, 6, 7, 8				},
				new [] {6, 5, 7, 8					},
				new [] {7, 1, 5, 6, 8				},
				new [] {8, 5, 6, 7					}

			}
			.Select(a => a.Select(i => i - 1).Skip(1).ToArray()).ToArray();

			var minCut = new KargerMinCut().CalcMinCut(input);

			Assert.AreEqual(2, minCut);
		}

		[TestMethod]
		public void CanFindMinCut_Graph3()
		{
			var input = new[]
			{							
				new [] {1,19,15,36,23,18,39,			   	},
				new [] {2,36,23,4,18,26,9	},
				new [] {3,35,6,16,11	},
				new [] {4,23,2,18,24	},
				new [] {5,14,8,29,21	},
				new [] {6,34,35,3,16	},
				new [] {7,30,33,38,28	},
				new [] {8,12,14,5,29,31	},
				new [] {9,39,13,20,10,17,2	},
				new [] {10,9,20,12,14,29	},
				new [] {11,3,16,30,33,26	},
				new [] {12,20,10,14,8	},
				new [] {13,24,39,9,20	},
				new [] {14,10,12,8,5	},
				new [] {15,26,19,1,36	},
				new [] {16,6,3,11,30,17,35,32	},
				new [] {17,38,28,32,40,9,16	},
				new [] {18,2,4,24,39,1	},
				new [] {19,27,26,15,1	},
				new [] {20,13,9,10,12	},
				new [] {21,5,29,25,37	},
				new [] {22,32,40,34,35	},
				new [] {23,1,36,2,4	},
				new [] {24,4,18,39,13	},
				new [] {25,29,21,37,31	},
				new [] {26,31,27,19,15,11,2	},
				new [] {27,37,31,26,19,29	},
				new [] {28,7,38,17,32	},
				new [] {29,8,5,21,25,10,27	},
				new [] {30,16,11,33,7,37	},
				new [] {31,25,37,27,26,8	},
				new [] {32,28,17,40,22,16	},
				new [] {33,11,30,7,38	},
				new [] {34,40,22,35,6	},
				new [] {35,22,34,6,3,16	},
				new [] {36,15,1,23,2	},
				new [] {37,21,25,31,27,30	},
				new [] {38,33,7,28,17,40	},
				new [] {39,18,24,13,9,1	},
				new [] {40,17,32,22,34,38	},
			}
			.Select(a => a.Select(i => i - 1).Skip(1).ToArray()).ToArray();

			var minCut = new KargerMinCut().CalcMinCut(input);

			Assert.AreEqual(3, minCut);
		}
	}
}