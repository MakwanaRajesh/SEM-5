#include<stdio.h>
#include<stdlib.h>
#include<time.h>
#define size 100000

void SelectionSort(int arr[]){
    for (int i = 0; i < size - 1; i++) {
      
        int min = i;
        
        for (int j = i + 1; j < size; j++) {
            if (arr[j] < arr[min]) {
              
                min = j;
            }
        }
        
        int temp = arr[i];
        arr[i] = arr[min];
        arr[min] = temp;
    }
    
    for(int i=0; i<size; i++){
        printf("%d\n", arr[i]);
    }
}

void main(){
    int arr[size];
    FILE* fp;
    clock_t start, end;
    double cpu_time;

    fp = fopen("File_Best.txt", "r");

    for(int i=0; i<size; i++){
        fscanf(fp, "%d", &arr[i]);
    }

    start = clock();
    SelectionSort(arr);
    end = clock();

    cpu_time = ((double)(end-start)) / CLOCKS_PER_SEC;

    printf("Time : %.2f", cpu_time);

}