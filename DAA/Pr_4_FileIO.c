#include<stdio.h>
#include<stdlib.h>

int main(){
    FILE* fp;
    FILE* fp1; 
    FILE* fp2;

    fp = fopen("./File_Best.txt", "w");
    fp1 = fopen("./File_Worst.txt", "w");
    fp2 = fopen("./File_Average.txt", "w");

    int i = 1;
    while(i <= 100000){
        fprintf(fp, "%d\n", i);
        fprintf(fp1, "%d\n", 100000 - i + 1);
        fprintf(fp2, "%d\n", rand());
        i++;
    }

    fclose(fp);
    fclose(fp1);
    fclose(fp2);
    printf("Done...");

    return 0;


}