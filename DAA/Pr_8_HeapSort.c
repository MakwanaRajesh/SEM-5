// #include <stdio.h>
// #include <stdlib.h>
// #include <time.h>
// #define size 10000

// void heapify(int arr[], int n, int i) {
//     int largest = i;
//     int l = 2 * i + 1;
//     int r = 2 * i + 2;

//     if (l < n && arr[l] > arr[largest]) {
//         largest = l;
//     }

//     if (r < n && arr[r] > arr[largest]) {
//         largest = r;
//     }

//     if (largest != i) {
//         int temp = arr[i];
//         arr[i] = arr[largest];
//         arr[largest] = temp;

//         heapify(arr, n, largest);
//     }
// }

// void heapSort(int arr[]) {
//     for (int i = size / 2 - 1; i >= 0; i--) {
//         heapify(arr, size, i);
//     }

//     for (int i = size - 1; i > 0; i--) {
//         int temp = arr[0];
//         arr[0] = arr[i];
//         arr[i] = temp;

//         heapify(arr, i, 0);
//     }
//     for (int i = 0; i < size; i++) {
//         printf("%d\n", arr[i]);
//     }
// }


// int main() {
//     int arr[size];
//     FILE* fp = fopen("File_Best.txt", "r");

//     for (int i = 0; i < size; i++) {
//         fscanf(fp, "%d", &arr[i]);
//     }
//     fclose(fp);

//     clock_t start, end;
//     double cpu_time;

//     start = clock();
//     heapSort(arr);
//     end = clock();

//     cpu_time = ((double)(end - start)) / CLOCKS_PER_SEC;

//     printf("Sorted array is\n");
//     printf("Time : %.2f seconds\n", cpu_time);

//     return 0;
// }


#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#define size 1000

void HeapSort(int arr[]){
    for(int i = size / 2 - 1 ; i>=0; i--){
        heapify(arr, size, i);  
    }

    for(int i = size)
}