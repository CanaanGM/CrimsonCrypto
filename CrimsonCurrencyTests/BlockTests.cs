using CrimsonCurrency.Data.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

namespace CrimsonCurrencyTests
{
    [TestClass]
    public class BlockTests
    {
        public Block block { get; set; }
        public List<Block> Data { get; set; } = new List<Block> { Block.Genesis() };
        public Block LastBlock { get; set; }  
        public Block CurrentBlock { get; set; } 

        [TestMethod]
        public void AssertGenesisBlockSameAsFirstBlockInBlock()
        {
            block = new Block();
            block.Data = new List<Block> {Block.Genesis() };
            Assert.AreEqual(block.Data[0].CurrentHash, Block.Genesis().CurrentHash);
        }

        [TestMethod]
        public void AssertGenesisNotEqualTameredGenesisInBlock()
        {
            block = new Block();
            block.Data = new List<Block> { Block.Genesis() };
            block.Data[0].CurrentHash = "";
            Assert.AreNotEqual(block.Data[0].CurrentHash, Block.Genesis().CurrentHash);
        }




        [TestMethod]
        public void AssertCreatedHashIsSameAsCurrentHashForBlock()
        {
            block = new Block(
                System.DateTime.UtcNow,
                Block.Genesis().CurrentHash,
                Block.Hash(System.DateTime.UtcNow, Block.Genesis().CurrentHash,Data ),
                Data
                );
            Data.Add(block);
            var block2 = Block.MineBlock(block, Data);

            Assert.AreEqual(block2.LastHash, block.CurrentHash);

        }

        [TestMethod]
        public void AssertHashBlockEqualCurrenthashForSameBlock()
        {
            block = new Block(
                System.DateTime.UtcNow,
                Block.Genesis().CurrentHash,
                Block.Hash(System.DateTime.UtcNow, Block.Genesis().CurrentHash, Data),
                Data
            );

            Assert.AreEqual(Block.HashBlock(block), block.CurrentHash);
        }

        [TestMethod]
        public void AssertSameHashIsGeneratedForSamePhrase()
         => Assert.AreEqual(Block.ComputeSha256Hash("Zoro"), Block.ComputeSha256Hash("Zoro"));
    }
}
