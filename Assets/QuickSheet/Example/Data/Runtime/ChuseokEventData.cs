using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class ChuseokEventData
{
  [SerializeField]
  int id;
  public int Id { get {return id; } set { this.id = value;} }
  
  [SerializeField]
  int itemtype;
  public int Itemtype { get {return itemtype; } set { this.itemtype = value;} }
  
  [SerializeField]
  float itemvalue;
  public float Itemvalue { get {return itemvalue; } set { this.itemvalue = value;} }
  
  [SerializeField]
  int limit;
  public int Limit { get {return limit; } set { this.limit = value;} }
  
  [SerializeField]
  string description;
  public string Description { get {return description; } set { this.description = value;} }
  
  [SerializeField]
  int price;
  public int Price { get {return price; } set { this.price = value;} }
  
  [SerializeField]
  string exchangekey;
  public string Exchangekey { get {return exchangekey; } set { this.exchangekey = value;} }
  
  [SerializeField]
  int exchangemaxcount;
  public int Exchangemaxcount { get {return exchangemaxcount; } set { this.exchangemaxcount = value;} }
  
}