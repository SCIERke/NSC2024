using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public struct CardData : INetworkSerializable
{
    public int id;
    public string cardName;
    public string description;
    public int workingPoints;
    public int actionPoints;
    public CardType cardType;
    public string imagePath;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref id);
        serializer.SerializeValue(ref cardName);
        serializer.SerializeValue(ref description);
        serializer.SerializeValue(ref workingPoints);
        serializer.SerializeValue(ref actionPoints);
        serializer.SerializeValue(ref cardType);
        serializer.SerializeValue(ref imagePath);
    }
}