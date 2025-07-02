#include<stdio.h>
#include<stdlib.h>
#include<time.h>
#define size 100000

void InsertionSort(int arr[]){
    for (int i = 1; i < size; ++i) {
        int key = arr[i];
        int j = i - 1;

        while (j >= 0 && arr[j] > key) {
            arr[j + 1] = arr[j];
            j = j - 1;
        }
        arr[j + 1] = key;
    }
    for(int i=0; i<size; i++){
        printf("%d\n", arr[i]);
    }
}

void main(){
    int arr[size];
    FILE* fp;

    fp = fopen("File_Best.txt", "r");

    for(int i=0; i<size; i++){
        fscanf(fp, "%d", &arr[i]);
    }

    clock_t start, end;
    double c_t_u;
    start = clock();
    InsertionSort(arr);
    end = clock();

    c_t_u = ((double)(end-start)) / CLOCKS_PER_SEC;
    printf("Time : %.2f", c_t_u);

}