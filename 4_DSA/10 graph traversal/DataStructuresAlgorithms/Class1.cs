﻿using System;
using System.Collections.Generic;

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
            // Узлы задаются позициями в списке vertex.
            // Возвращается список узлов -- путь из VFrom в VTo.
            // Список пустой, если пути нету.
        }

    }
}

