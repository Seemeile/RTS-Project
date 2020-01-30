﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;


public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    public Mesh demonMesh;
    public Material demonMaterial;
    public Transform selectionAreaTransform;
    public Material unitSelectedCircleMaterial;
    public Mesh unitSelectedCircleMesh;

    private EntityManager entityManager;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
      entityManager = World.Active.EntityManager;

        for (int i=0; i < 2; i++)
        {
            SpawnDemon();
        }
        
    }

    private void SpawnDemon()
    {
        SpawnDemon(new float3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f), 0f));
    }
    private void SpawnDemon(float3 spawnPosition)
    {
        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Demon),
            typeof(Translation),
            typeof(MoveTo),
            typeof(RenderMesh),
            typeof(LocalToWorld)
             );

        Entity entity = entityManager.CreateEntity(entityArchetype);

        entityManager.SetComponentData(entity, new Translation { Value = spawnPosition });
        entityManager.SetComponentData(entity, new MoveTo { move = true, position = spawnPosition, moveSpeed = 40f });
        entityManager.SetSharedComponentData(entity, new RenderMesh
        {
            mesh = demonMesh,
            material = demonMaterial,
        });
    }








    public struct Demon : IComponentData { }

    public struct MoveTo : IComponentData
    {
        public bool move;
        public float3 position;
        public float3 lastMoveDir;
        public float moveSpeed;
    }
}
