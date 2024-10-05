package main

import (
    "fmt"
    "os"
    "bufio"
    "math"
    //"sort"
    //"strings"
    )
	
func main() {
buf_reader := bufio.NewReaderSize(os.Stdin, 512)
buf_writer := bufio.NewWriterSize(os.Stdout, 256)
var k int

fmt.Fscanf(buf_reader,"%d", &k)

data := make([]int,k+500)

for i:=0;i<k+500;i+=100 {
data[i] = i/100
}

for i:=0;i<k+400;i+=200 {
data[i] = i/200
data[i+100]=i/200 +1
}

for i:=0;i<k+500;i+=500 {
data[i] = i/500
if i<k+400 {
data[i+100] = i/500 + 1
}
if i<k+300 {
data[i+200] = i/500 + 1
}

if i<k+200 {
data[i+300] = i/500 + 2
}

if i<k+100 {
data[i+300] = i/500 + 2
}

}

if data[k]>0 {
buf_writer.WriteString(fmt.Sprintf("%v",data[k]))
} else {
rem1:= 10000
rem2:= 10000
left:=0;
right:=0;
for i:=k;i<k+500&&right*left==0;i++ {

if right == 0 && data[i] > 0{
right = data[i]
rem1 = int(math.Min(float64(rem1),float64(i-k)))
}
if left == 0 && 2*k-i > 0 {
left = data[2*k-i]
rem2 = int(math.Min(float64(rem2),float64(k+100-i)))
}

if left ==0 && 2*k-i <= 0 {
left = 10000
}

}

if rem1 < rem2 {
buf_writer.WriteString(fmt.Sprintf("%v",right))
} else {
buf_writer.WriteString(fmt.Sprintf("%v",left))
}

}
buf_writer.Flush()
}