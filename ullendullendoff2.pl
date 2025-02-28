:-use_module(moduleMyNumbers, [readUInt/1]).
:-use_module(moduleBufferedIO, [initiateBufferedRead/1,bufferedWrite/1,bufferedWriteln/1]).

writeList([]):-!,nl.
writeList([H|T]):-char_code(Ch,H),bufferedWrite(Ch),writeList(T).

writeName(I):-name(I,Name),writeList(Name).

writeData(N,N,_).
writeData(I,N,Add):-II is I+1,(place(I),!,writeName(0),NewAdd is 0;K is I+Add,writeName(K),NewAdd is Add),writeData(II,N,NewAdd).

getName(Current,Result):-at_end_of_stream,!,Result = Current.
getName(Current,Result):-get0(Ch),(Ch is 10,!,Result = Current;append(Current,[Ch],NewCurrent),getName(NewCurrent,Result)).

readData(N,N).
readData(I,N):-II is I+1,getName([],Name),assert(name(I,Name)),readData(II,N).

main:-initiateBufferedRead(16384),readUInt(N),readData(0,N),(N>=13,!,assert(place(12));K is 13 rem N,KK is K-1,assert(place(KK))),writeData(0,N,1).