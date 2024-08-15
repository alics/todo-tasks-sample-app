import { signal } from '@angular/core'; // Assuming 'signal' is a custom utility or function for managing state
import { isEqual } from 'lodash-es'; // Importing isEqual function from lodash-es for deep comparison
import { DEFAULT_DATASOURCE_STATE } from '../constants/data-source.constant'; // Importing default state constant from a custom constant file
import { DataSourceState } from '../dto/data-source.dto'; // Importing DataSourceState DTO from a custom DTO file

/**
 * Represents a generic data source with state management.
 * @template T The type of data stored in the DataSource
 */
export class DataSource<T> {
  /**
   * Signal to hold the current state of the data source.
   * Initialized with DEFAULT_DATASOURCE_STATE.
   */
  public state = signal<DataSourceState>(DEFAULT_DATASOURCE_STATE);

  /**
   * Signal to hold the current error message state.
   * Initialized with an empty string.
   */
  public error = signal('');

  /**
   * Signal to hold the current data state.
   * Initialized with the provided initialData.
   */
  public data = signal<T>(this.initialData);

  /**
   * Constructor to initialize a new instance of DataSource.
   * @param initialData Initial data of type T for the DataSource
   */
  constructor(private initialData: T) {}

  /**
   * Updates the state based on current error and data conditions.
   * Sets 'error', 'empty', or 'data' state based on current conditions.
   */
  public updateState(): void {
    if (this.error()) {
      this.state.set('error');
    } else if (isEqual(this.data(), this.initialData)) {
      this.state.set('empty');
    } else {
      this.state.set('data');
    }
  }

  /**
   * Sets the error message, resets data to initialData, and updates state accordingly.
   * @param message Error message to set
   */
  public setError(message: string): void {
    this.error.set(message);
    this.data.set(this.initialData);
    this.updateState();
  }

  /**
   * Sets new data for the DataSource.
   * If no data provided, resets data to initialData.
   * Updates state based on the new data.
   * @param data Optional new data of type T
   */
  public setData(data?: T): void {
    this.data.set(data || this.initialData);
    this.updateState();
  }
}
