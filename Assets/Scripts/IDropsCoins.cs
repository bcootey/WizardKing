using UnityEngine;

public interface IDropsCoins
{
   int CoinsMin { get; set; }
   int CoinsMax { get; set; }
   public void DropCoins();
}
