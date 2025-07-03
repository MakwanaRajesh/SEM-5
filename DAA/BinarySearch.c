#include <stdio.h>
#include<stdlib.h>
#include <time.h>
#define size 10000

int BinarySearch(int arr[], int key){
    int left = 0;
    int right = size - 1;

    while (left <= right)
    {
        int mid = (left + right )/ 2;

        if (arr[mid] == key)
        {
            return  mid;
        }
        else if (key < mid)
        {
            right = mid -1;
        }
        else {
            left = mid +1;
        }
        return -1;
        
        
    }
}

int main(){
    int arr[size];
    FILE* fp;
    clock_t start, end;
    double cpu_time;

    fp = fopen("Best.txt","r");
    for (int i = 0; i < size ; i++)
    {
        fscanf(fp, "%d", &arr[i]);
    }
    fclose(fp);

    printf("Enter Key : ");
    int key;
    scanf("%d", &key);


    start = clock();
    int res = BinarySearch(arr, key);
    end = clock();

    cpu_time = (double)(end - start) / CLOCKS_PER_SEC;

    if (res != -1)
    {
        printf("%d", res);
    }
    else{
        printf("Not Found 404()");
    }
    printf("Time : %.2f " , cpu_time);
    
}