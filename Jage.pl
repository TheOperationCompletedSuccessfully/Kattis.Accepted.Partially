readUInt(Number):-readUInt(-1,Number).
readUInt(-1,R):-!,get0(Ch),(Ch <48,!,readUInt(-1,R);I1 is Ch-48,readUInt(I1,R)).
readUInt(I,R):-get0(Ch),(Ch <48,!,R is I;I1 is I*10+Ch-48,readUInt(I1,R)).

processFirst(I):-hunter(I),!,retract(hunter(I)).
processFirst(I):-cheater(I),!,true.
processFirst(I):-assert(cheater(I)).

getName(Current,Result):-get0(Ch),(Ch =<32,!,Result = Current;append(Current,[Ch],NewC),getName(NewC,Result)).

processEvents(N,N).
processEvents(I,N):-fill_buffer(user_input),II is I+1,getName([],FirstStudent),getName([],_),getName([],SecondStudent),student(F,FirstStudent),processFirst(F),student(S,SecondStudent),(hunter(S),!,true;assert(hunter(S))),processEvents(II,N).

readStudents(N,N).
readStudents(I,N):-II is I+1,getName([],Name),assert(student(I,Name)),readStudents(II,N).

getCheater(Cheater):-cheater(I),student(I,Cheater).

writeName([]).
writeName([H|T]):-char_code(Ch,H),bufferedWrite(Ch),writeName(T).

writeItems([]).
writeItems([H|T]):-writeName(H),(T=[],!,true;bufferedWrite(' ')),writeItems(T).

findAnswer:-(cheater(C),C>=0,!,findall(Cheater,getCheater(Cheater),L),msort(L,Sorted),length(L,Len),bufferedWriteln(Len),writeItems(Sorted);bufferedWriteln(0)).

bufferedWrite(C):-with_output_to(user_output,write(C)).
bufferedWriteln(C):-with_output_to(user_output,writeln(C)).

main:-set_stream(user_input,buffer_size(2097152)),fill_buffer(user_input),readUInt(Students),readUInt(Events),readStudents(0,Students),assert(hunter(0)),assert(cheater(-1)),processEvents(0,Events),findAnswer.