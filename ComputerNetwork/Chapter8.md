	- Secure commnication
		○ Confidentiality
		○ Message integrity
		○ End-point authentication
		○ Operational security
	- Principles of Cryptography
		○ Symmetric key cryptography
			§ Ciphertext-only attack
			§ Known-plaintext attack
			§ Chosen-plaintext attack
			§ RSA
			§ Sessions key
	- Message integrity and Digital signature
		○ Authentication key
		○ Public key certification: IPsec, SSL
	- End-point authentication
		○ nonce
	- Securing email
		○ PGP : pretty good privacy
	- Securing tcp connections: SSL
		○ Handshake
		○ Key derivation
		○ Data transfer
		○ SSL Record
	- Network-layer security:IPsec and Vitual private networks
		○ IPsec: authentication header, encapsulation security payload
	- Securing wireless lans
		○ WEP: wireless equavalent privacy
		○ IEEE 802.1 1i
		1. A wireless host requests authentication by an access point.
		2. The access point responds to the authentication request with a 128-byte nonce
		value.
		3. The wireless host encrypts the nonce using the symmetric key that it shares
		with the access point.
		4. The access point decrypts the host-encrypted nonce.
		
	- Operational security: firewalls and intrusion detection systems
		○ Firewalls
			§ All traffic from outside to inside, and vice versa, passes through the firewall
			§ Only authorized traffic, as defined by the local security policy, will be allowed to pass
The firewall itself is immune to penetration
