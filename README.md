<h1 align="center">Ontology C# SDK </h1>

## Overview

This C# SDK aims to help .NET developers when writing applications for the Ontology Blockhain.  

<b> Note: This software should be considered pre-alpha and only be used on testnet (current version 0.75, address: polaris1.ont.io ) or privatenet</b>

<br><br>
## Setup / Usage

Requires .NET v4.7

1. Download repository
```
https://github.com/OntologyCommunityDevelopers/ontology-csharp-sdk.git
```
2. Build the repository, ddl files should be available in `ontology-csharp-sdk\ontology-csharp-sdk\bin\Debug folder`.

3. Add the ddl files in your client project, invoke the library

```
using OntologyCSharpSDK;
```

4. Create instance by specifying node parameter and ConnectionMethod (RPC, REST or Websocket), e.g.
```
OntologySDK ontSDK = new OntologySDK(node, ConnectionMethod.REST);
```
node parameter is constructed by domain/IP + port

| Network | Port |
| :---| :---|
| REST | 20334|
| Websocket | 20335|
| RPC | 20336|

For domain/IP, use current testnet address `http://polaris1.ont.io` or your private net address

For example, calling REST method for testnet(currently 0.75) will be below:
```
OntologySDK ontSDK = new OntologySDK("http://polaris1.ont.io:20334", ConnectionMethod.REST);
```

5. Call methods, e.g.
```
int BlockHeight = ontSDK.connectionManager.getBlockHeight();
```

See more code examples in `ontology-csharp-demo` project

<br><br>
## Methods

| Method | Parameters | Description | Note |
| :---| :---| :---| :---|
| getBlockGenerationTime |  | gets the current block generation time |  |
| getBlockHeight |  | gets the current block height | |
| getAddressBalance | string | gets the ont/ong balance of address |  |
| getNodeCount |  | gets the number of network nodes |  |
| getBlockHeightByTxHash | string | gets the block height of specified transaction hash |  |
| getBlockHex | int | gets the block (hex) of specified block by block height | |
| getBlockHex | string | gets the block (hex) of specified block by block hash | |
| getBlockJson | int | gets the block (json) of specified block by block height| |
| getBlockJson | string | gets the block (json) of specified block by block hash | |
| getRawTransactionHex | string | gets the hex representation of a transaction based on transaction hash | |
| getRawTransactionJson | string | gets the json representation of a transaction based on transaction hash | |



<br><br>
## Account

<b>createPrivateKey</b>: create a private key using SecureRandom

<b>getPublicKey</b>: get a public key from a private key
- privatekey (string)

<b>createONTID</b> create a ONTID from a private key
- privatekey (string)

<b>registerONTID</b> register a ONTID on Blockchain
- ontid (string)
- privatekey (string)

<b>transferFund</b>: Transfer token from address to another address
- name (string) - name of the fund, for example: ONT
- fromaddress (string) from address
- toaddress (string) to address
- value (decimal) value of the fund
- privatekey (string) private key of the from address

<b>registerClaim</b>: issue and register a claim
- context (string) - context (template name) for the claim, it can be standard or self-defined
- metadata (json string) metadata for the claim, including issuer and subject, expiry etc
- content (json string) json data for the claim
- type (string) JSON or string
- issuer (string) ONTID of the party is registering this claim
- privatekey (string) private key of the issuer

<b>addPublicKey</b> add a public key to existing ONTID
- ontid (string) existing ONTID
- new_publickey (string) new public key need to be added
- publickey (string) public key for the existing private key
- privatekey (string) existing private key


<br><br>
## License

This project is licensed under the GNU GENERAL PUBLIC LICENSE v3.0

<br><br>
## Current Contributors

- Panther

Github: https://github.com/panther142
Discord: Panther#3489

- Bobio

Github: https://github.com/bobio2018
Discord: Bobio#7026

<br><br>
## Suggestions and report issues
Issues can be submitted directly in Github or join our discussion on Discord

- Ontology official Discord:https://discord.gg/mggatmY
- Ontology Community Developers Discord: https://discord.gg/w9WCPsd
