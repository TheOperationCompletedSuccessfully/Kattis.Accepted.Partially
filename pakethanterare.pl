:-use_module(moduleMyNumbers, [readUInt/1]).
:-use_module(moduleBufferedIO, [initiateBufferedRead/1,bufferedWrite/1,bufferedWriteln/1]).

readName(Current,Name):-get0(Ch),(Ch<97,!,Name = Current;append(Current,[Ch],NewC),readName(NewC,Name)).

readStoreMetaData(N,N).
readStoreMetaData(I,N):-II is I+1,readUInt(NextMetaData),assert(store(I,NextMetaData)),readStoreMetaData(II,N).

readBaselineData(N,N).
readBaselineData(I,N):-II is I+1,readName([],Name),readUInt(LatestVersion),assert(latestVersion(Name,LatestVersion)),readBaselineData(II,N).

processStore(N,N,Result,Result).
processStore(I,N,Current,Result):-II is I+1,readName([],NextName),readUInt(NextVersion),latestVersion(NextName,LatestVersion),NewC is Current+LatestVersion-NextVersion,processStore(II,N,NewC,Result).

processStores(N,N).
processStores(I,N):-II is I+1,store(I,Limit),processStore(0,Limit,0,Result),writeln(Result),processStores(II,N).

main:-initiateBufferedRead(16384),readUInt(T),readUInt(B),readStoreMetaData(0,B),readBaselineData(0,T),processStores(0,B).