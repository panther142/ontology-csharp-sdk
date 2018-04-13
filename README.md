<h1 align="center">Ontology C# SDK </h1>
<h4 align="center">Version 0.0.1 </h4>

## Overview

This C# SDK aims to help .NET developers when writing applications for the Ontology Blockhain.  The initial implementation supports RPC.

<b> Note: This software should be considered pre-alpha and only be used on testnet or privatenet </b>

<br><br>
## Setup / Usage

Requires .NET v4.7

1. Download repository
```
https://github.com/OntologyCommunityDevelopers/ontology-csharp-sdk.git
```
2. Modify Network/RPCrequest.cs to point to the appropriate RPC node
```
HttpWebRequest ontRPCRequest = (HttpWebRequest)WebRequest.Create("http://ont-privnet:20336"); <--- modify as needed
```
3. Build and add ontology-csharp-sdk.dll to your project

4. Create instance, e.g.
```
OntologySDK ontSDK = new OntologySDK();
```
5. Call methods, e.g.
```
int BlockHeight = ontSDK.getBlockHeight();
```

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
| createPrivateKey |  | create a private key using SecureRandom  | |
| getPublicKey | string | get a public key from a private key  | |

<br><br>
## License

This project is licensed under the GNU GENERAL PUBLIC LICENSE v3.0
