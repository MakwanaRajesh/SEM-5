    #include<stdio.h>
    #include<stdlib.h>
    #include<time.h>
    #define size 100000

    void BubbleSort(int arr[]){
        int i, j, temp;
        for(int i=0; i < size-1; i++){
            for(int j=0; j < size-1; j++){
                if(arr[j] > arr[j+1]){
                    temp = arr[j];
                    arr[j] = arr[j+1];
                    arr[j+1] = temp;
                }
            }
        }

        for(int a = 0; a<size;a++ ){
            printf("%d\n",arr[a]);
        }
    }

    int main(){
        int arr[size];
        FILE* fp1;
        clock_t start, end;
        double cpu_time_used;
        fp1 = fopen("File_Best.txt", "r");

        for(int i1 = 0; i1 < size; i1++){
            fscanf(fp1, "%d", &arr[i1]);
        }
        fclose(fp1);
        start = clock(); 
        BubbleSort(arr);
        end = clock();

        cpu_time_used = ((double)(end - start)) / CLOCKS_PER_SEC;

        printf("Time : %.2f", cpu_time_used);
        return 0;
        
        
    }
