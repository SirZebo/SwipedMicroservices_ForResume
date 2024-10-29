using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class ConsistentHashRing
{
    private readonly SortedDictionary<int, string> ring = new SortedDictionary<int, string>();
    private readonly int replicas;

    public ConsistentHashRing(IEnumerable<string> nodes, int replicas = 3)
    {
        this.replicas = replicas;
        foreach (var node in nodes)
        {
            AddNode(node);
        }
    }

    private int GetHash(string key)
    {
        using (var md5 = MD5.Create())
        {
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            return BitConverter.ToInt32(hashBytes, 0);
        }
    }

    public void AddNode(string node)
    {
        for (int i = 0; i < replicas; i++)
        {
            var virtualNodeId = $"{node}:{i}";
            var hash = GetHash(virtualNodeId);
            ring[hash] = node;
        }
    }

    public void RemoveNode(string node)
    {
        for (int i = 0; i < replicas; i++)
        {
            var virtualNodeId = $"{node}:{i}";
            var hash = GetHash(virtualNodeId);
            ring.Remove(hash);
        }
    }

    public string GetNode(string key)
    {
        if (!ring.Any()) return null;

        var hash = GetHash(key);
        if (!ring.ContainsKey(hash))
        {
            var nearestNode = ring.Keys.FirstOrDefault(k => k > hash);
            return nearestNode == 0 ? ring.First().Value : ring[nearestNode];
        }
        return ring[hash];
    }

}
