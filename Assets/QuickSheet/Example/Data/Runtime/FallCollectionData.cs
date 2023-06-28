using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class FallCollectionData
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
  float price;
  public float Price { get {return price; } set { this.price = value;} }
  
  [SerializeField]
  string exchangekey;
  public string Exchangekey { get {return exchangekey; } set { this.exchangekey = value;} }
  
  [SerializeField]
  int exchangemaxcount;
  public int Exchangemaxcount { get {return exchangemaxcount; } set { this.exchangemaxcount = value;} }
  
  [SerializeField]
  bool active;
  public bool Active { get {return active; } set { this.active = value;} }
  
}