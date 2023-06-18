# README 

For the design patterns in our game, specific instances are commented directly into the code but as our project is a little complex I thought it would be good to provide a short overview: 

  * Pipes and filters design pattern (Structural) is used through out the entire project but some specific instances are: 
    * Chunk.cs line 57
    * Chunk.cs line 75


  * Data Parallelism design pattern (Concurrency): 
    * GridMetrics.cs line 4 
    * MarchingCubesCompute.compute line 37
    * MarchingCubesCompute.compute line 94


  * Strategy design pattern (Behavioral):
    * MarchingCubes.compute (entire computeshader) 
    
  * Game Controls:
   
    * WASD for basic movement
    * Space for Jump
    * Left-click while looking at the terrain to add terrain
    * Right-click while looking at the terrain to remove terrain
