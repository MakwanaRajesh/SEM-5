#include <stdio.h>
#include <limits.h>

#define V 7

int prims(int graph[V][V], int n) {
    int parent[V]; 
    int key[V];    
    int mstSet[V];

    for (int i = 0; i < n; i++) {
        key[i] = INT_MAX;
        mstSet[i] = 0;
    }


    key[0] = 0;     
    parent[0] = -1; 
    for (int count = 0; count < n - 1; count++) {

        int min = INT_MAX, u;

        for (int v = 0; v < n; v++) {
            if (mstSet[v] == 0 && key[v] < min) {
                min = key[v];
                u = v;
            }
        }

        mstSet[u] = 1;

        for (int v = 0; v < n; v++) {
            if (graph[u][v] && mstSet[v] == 0 && graph[u][v] < key[v]) {
                parent[v] = u;
                key[v] = graph[u][v];
            }
        }
    }

    printf("Edge \tWeight\n");
    for (int i = 1; i < n; i++) {
        printf("%d - %d \t%d \n", parent[i], i, graph[i][parent[i]]);
    }

    return 0;
}

int main() {
    int graph[V][V] = {
        {0,1,0,4,0,0,0},
        {1,0,2,6,4,0,0},
        {0,2,0,0,5,6,0},
        {4,6,0,0,3,0,4},
        {0,4,5,3,0,8,7},
        {0,0,6,0,8,0,3},
        {0,0,0,4,7,3,0}
    };

    prims(graph, V);

    return 0;
}
