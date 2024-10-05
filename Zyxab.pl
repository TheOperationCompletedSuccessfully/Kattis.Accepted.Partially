readUInt(Number):-readUInt(-1,Number).
readUInt(-1,R):-!,get0(Ch),(Ch <48,!,readUInt(-1,R);I1 is Ch-48,readUInt(I1,R)).
readUInt(I,R):-get0(Ch),(Ch <48,!,R is I;I1 is I*10+Ch-48,readUInt(I1,R)).

p([],_,_,_).
p([Head|Tail],P,S,B):-
    (  Head = [N|_],P = [PN|_],(N>PN)->S=[Head|R],p(Tail,P,R,B);B=[Head|R],p(Tail,P,S,R)).
sortList([],Result):-Result =[].
sortList([Head|Tail],Result):-p(Tail,Head,S,B),sortList(S,SR),sortList(B,BR),append(SR,[Head],SR1),append(SR1,BR,Result).

readPendingLine:-at_end_of_stream,!,true.
readPendingLine:-get0(Ch),(Ch is 10,!,true;readPendingLine).

readData(N,N,_,_,_,Current,Result):-!,Result=Current.
readData(_,N,W,WMin,CurrentWord,Current,Result):-at_end_of_stream,!,(W>4,!,(W is WMin,!,append(Current,[CurrentWord],NewC),NewW is WMin;W<WMin,!,NewC=[CurrentWord],NewW is W;NewC=Current,NewW is WMin);NewC=Current,NewW is WMin),readData(N,N,0,NewW,[],NewC,Result).
readData(I,N,W,WMin,CurrentWord,Current,Result):-get0(Ch),(Ch is 10,!,(W>4,!,(W is WMin,!,append(Current,[CurrentWord],NewC),NewW is WMin;W<WMin,!,NewC=[CurrentWord],NewW is W;NewC=Current,NewW is WMin);NewC=Current,NewW is WMin),I1 is I+1,readData(I1,N,0,NewW,[],NewC,Result);(nth0(_,CurrentWord,Ch),!,readPendingLine,I1 is I+1,readData(I1,N,0,WMin,[],Current,Result);append(CurrentWord,[Ch],NewCW),W1 is W+1,readData(I,N,W1,WMin,NewCW,Current,Result))).

writeAnswer([]):-!,writeln('neibb!').
writeAnswer([Head|_]):-!,printList(Head).

printList([]).
printList([H|T]):-char_code(Ch,H),write(Ch),printList(T).


main:-readUInt(N),readData(0,N,0,20,[],[],Result),sortList(Result,Sorted),writeAnswer(Sorted).