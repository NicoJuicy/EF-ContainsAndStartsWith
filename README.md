# EF-ContainsAndStartsWith

This project was created as part of a request of performance improvement on EF. In an edge-case of EF a slow query is generated, while there is an alternative ( difference of 10 seconds vs 1 second)

[see the request here](https://github.com/aspnet/EntityFramework6/issues/115)

It led to a additional Like operator in DbFunctions that can be used.


