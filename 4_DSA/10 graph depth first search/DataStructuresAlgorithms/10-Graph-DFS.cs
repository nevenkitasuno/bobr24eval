﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsDataStructures2
{
    public class Vertex<T>
    {
        public bool Hit;
        public T Value;
        public Vertex(T val)
        {
            Value = val;
            Hit = false;
        }
    }

    public class SimpleGraph<T>
    {
        public Vertex<T>[] vertex;
        public int[,] m_adjacency;
        public int max_vertex;

        public SimpleGraph(int size)
        {
            max_vertex = size;
            m_adjacency = new int[size, size];
            vertex = new Vertex<T>[size];
        }

        public void AddVertex(T value)
        {
            bool isPlaceFound = false;

            for (int i = 0; i < max_vertex && !isPlaceFound; i++)
            {
                if (vertex[i] == null)
                {
                    vertex[i] = new Vertex<T>(value);
                    isPlaceFound = true;
                }
            }
        }

        // здесь и далее, параметры v -- индекс вершины
        // в списке  vertex
        public void RemoveVertex(int v)
        {
            if (InternalIsVertex(v))
            {
                vertex[v] = null;
                for (int i = 0; i < max_vertex; i++) RemoveEdge(v, i);
            }
        }

        public bool IsEdge(int v1, int v2) => InternalIsVertex(v1, v2) && m_adjacency[v1, v2] != 0;

        public void AddEdge(int v1, int v2)
        {
            if (InternalIsVertex(v1, v2))
            {
                m_adjacency[v1, v2] = 1;
                m_adjacency[v2, v1] = 1;
            }
        }

        public void RemoveEdge(int v1, int v2)
        {
            if (InternalIsVertex(v1, v2))
            {
                m_adjacency[v1, v2] = 0;
                m_adjacency[v2, v1] = 0;
            }
        }

        private bool InternalIsVertex(int v) => 0 <= v && v < max_vertex && vertex[v] != null;
        private bool InternalIsVertex(int v1, int v2) => InternalIsVertex(v1) && InternalIsVertex(v2);

        public List<Vertex<T>> DepthFirstSearch(int VFrom, int VTo)
        {
            List<Vertex<T>> path = new();
            foreach (Vertex<T> vtx in vertex) if (vtx != null) vtx.Hit = false;
            InternalPickVertex(path, VFrom);
            bool isPathFound = InternalSeekVTo(path, VTo);
            // Узлы задаются позициями в списке vertex.
            // Возвращается список узлов -- путь из VFrom в VTo.
            // Список пустой, если пути нету.
            if (!isPathFound) return new();
            return path;
        }
        
        private void InternalPickVertex(List<Vertex<T>> path, int v)
        {
            vertex[v].Hit = true;
            path.Add(vertex[v]);
        }
        private int InternalFindNextPossible(int current)
        {
            int nextPossible = -1;
            for (int i = 0, vertexCount = vertex.Count(); i < vertexCount; i++)
                if (nextPossible == -1 && IsEdge(i, current) &&!vertex[i].Hit) nextPossible = i;
            return nextPossible;
        }
        private bool InternalSeekVTo(List<Vertex<T>> path, int VTo)
        {
            int current = path.Count - 1;

            if (IsEdge(current, VTo))
            {
                path.Add(vertex[VTo]);
                return true;
            }

            int nextPossible = InternalFindNextPossible(current);
            if (nextPossible != -1)
            {
                InternalPickVertex(path, nextPossible);
                return InternalSeekVTo(path, VTo);
            }

            path.RemoveAt(path.Count - 1);

            if (path.Count == 0) return false;

            return InternalSeekVTo(path, VTo);
        }

    }
}

