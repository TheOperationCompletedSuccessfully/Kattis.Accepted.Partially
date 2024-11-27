:-use_module(moduleMyNumbers, [readUInt/1]).
:-use_module(moduleBufferedIO, [initiateBufferedRead/1,bufferedWrite/1,bufferedWriteln/1]).
:-use_module(moduleMinHeapV3,[minHeapPop/2,minHeapPush/3,minHeapPeek/2,minHeapPrintAll/1,minHeap/1,minHeapLength/2]).

:-dynamic processed/4.
:-dynamic path/3.
:-dynamic obsolete/3.

readBookPrices(N,N,_).
readBookPrices(I,N,Store):-II is I+1,readUInt(NextBook),readUInt(NextPrice),bookStore(Store,PostalFee),assert(book(NextBook,NextPrice,Store,PostalFee)),readBookPrices(II,N,Store).

readBookShops(N,N).
readBookShops(I,N):-II is I+1,readUInt(Books),readUInt(PostageFee),assert(bookStore(II,PostageFee)),readBookPrices(0,Books,II),readBookShops(II,N).

processBooks(HeapName,Next,Priority,ShopCode):-(book(Next,NextPrice,Store,PostalFee),not(processed(Next,NextPrice,Store,PostalFee)),!,Test is ShopCode rem 2^(Store+1),(Test >= 2^Store,!,NewPriority is Priority+NextPrice,NewShopCode is ShopCode;NewPriority is Priority + NextPrice + PostalFee,NewShopCode is ShopCode + 2^Store),NN is Next+1,(path(NN,OtherPriority,NewShopCode),!,(OtherPriority=<NewPriority,!,true;assert(path(NN,NewPriority,NewShopCode)),assert(obsolete(NN,OtherPriority,NewShopCode)),minHeapPush(HeapName,path(NN,NewPriority,NewShopCode),NewPriority));assert(path(NN,NewPriority,NewShopCode)),minHeapPush(HeapName,path(NN,NewPriority,NewShopCode),NewPriority)),assert(processed(Next,NextPrice,Store,PostalFee)),processBooks(HeapName,Next,Priority,ShopCode);true).

processHeap(HeapName,StopAt,Result):-minHeapPeek(HeapName,path(StopAt,Result,_)),!,true.
processHeap(HeapName,StopAt,Result):-minHeapPop(HeapName,path(Next,Priority,ShopCode)),(not(obsolete(Next,Priority,ShopCode)),!,processBooks(HeapName,Next,Priority,ShopCode),retractall(processed(_,_,_,_));true),processHeap(HeapName,StopAt,Result).

main:-initiateBufferedRead(16384),readUInt(Books),readUInt(BookShops),readBookShops(0,BookShops),minHeap('MyHeap'),minHeapPush('MyHeap',path(1,0,0),0),BB is Books+1,processHeap('MyHeap',BB,Result),bufferedWriteln(Result).