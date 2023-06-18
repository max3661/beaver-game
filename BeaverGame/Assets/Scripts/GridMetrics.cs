public static class GridMetrics {

    //example of data parallelism (concurency pattern) as our Marchingshader compute shader is using several threads to efficiently handle all necessary calculations 
    public const int NumThreads = 8;

    public const int Scale = 32;

    public const int GroundLevel = Scale / 2;

    public static int[] LODs = {
        8,
        16,
        24,
        32,
        40
    };


    public static int LastLod = LODs.Length - 1;

    public static int PointsPerChunk(int lod) {
        return LODs[lod];
    }

    public static int ThreadGroups(int lod) {
        return LODs[lod] / NumThreads;
    }
}