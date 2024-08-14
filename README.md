# BankSystem

This project is a comprehensive implementation of a banking system, focusing on the management of various financial transactions through an object-oriented approach. The system is designed to handle deposits, withdrawals, and transfers efficiently while maintaining a clear and organized code structure.

#Key Features
Abstract Transaction Class:

The Transaction class serves as a base for all transaction types, encapsulating common logic and attributes.
It includes abstract methods and properties, such as Execute, Rollback, and Success, which are implemented by derived classes.
Manages essential transaction details like _amount, _datestamp, and status.
Derived Transaction Classes:

DepositTransaction, WithdrawTransaction, and TransferTransaction are specialized classes inheriting from Transaction.
Each class implements the necessary functionality for executing and rolling back the respective transactions.
Bank Class Structure:

The Bank class manages all transactions through a private _transactions list, which stores instances of the Transaction class.
Provides a unified method ExecuteTransaction to handle the execution of any transaction type.
Includes a RollbackTransaction method to reverse transactions when necessary.
Transaction History Management:

A PrintTransactionHistory method allows users to view a detailed history of all transactions, including their status and timestamps.
The system enables users to roll back specific transactions directly from the history, ensuring flexibility and control.
